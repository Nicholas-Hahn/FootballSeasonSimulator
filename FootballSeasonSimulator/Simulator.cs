using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSeasonSimulator
{
    internal class Simulator
    {
        int lastSelection;
        int currentWeek;
        League league;
        List<List<Game>> regularSeason;


        public Simulator()
        {
            lastSelection = 0;
            currentWeek = 1;
            league = new League();
            regularSeason = new List<List<Game>>();
        }

        public void Execute()
        {
            league.BuildLeague();
            Loop();
        }

        private void Loop()
        {
            //TODO while loop for restart
            MainMenu();
            if (lastSelection == 9) return;
            GenerateSeason();

            while (currentWeek <= regularSeason.Count)
            {
                WeekStart(currentWeek);
                SimulateWeek(currentWeek);
                CurrentStandings();

                currentWeek += 1;
            }

            league.Nfc.FillPlayoffSeeds();
            league.Afc.FillPlayoffSeeds();
            DisplayPlayoffsBracket();
            PlayoffsWildcard();
            PlayoffsDivisional();
            PlayoffsConference();
            Superbowl();
        }

        private void MainMenu()
        {
            lastSelection = 0;

            while (lastSelection == 0)
            {
                Console.Clear();
                Console.WriteLine("=========================\n"
                                + "FOOTBALL SEASON SIMULATOR\n"
                                + "=========================\n");

                Console.WriteLine("OPTIONS:\n"
                                + "1. Start Season\n"
                                + "9. Quit\n");

                var input = Console.ReadLine();

                if (input == null || !int.TryParse(input, out lastSelection)) lastSelection = 0;
                if (lastSelection != 1 && lastSelection != 9) lastSelection = 0;
            }
        }

        private void WeekStart(int weekNumber)
        {
            Console.Clear();
            Console.WriteLine("------\n"
                            + "WEEK " + weekNumber + "\n"
                            + "------\n");

            DisplayWeekSchedule(weekNumber);
            Console.WriteLine();

            Console.WriteLine("\nPress ENTER to Continue\n");

            var input = Console.ReadLine();
        }

        private void GenerateSeason()
        {
            for (int i = 0; i < 18; i++)
            {
                regularSeason.Add(GenerateWeek());
            }
        }

        private List<Game> GenerateWeek()
        {
            List<Game> weekSchedule = new List<Game>();
            List<Team> scheduledTeams = new List<Team>();

            Random random = new Random();

            for (int i = 0; i < league.Teams.Count; i++)
            {
                if (scheduledTeams.Contains(league.Teams[i])) continue;

                int opponentIndex = -1;
                while (opponentIndex == -1)
                { 
                    int newIndex = random.Next(32);
                    if (newIndex != i && !scheduledTeams.Contains(league.Teams[newIndex]))
                        opponentIndex = newIndex;
                }

                scheduledTeams.Add(league.Teams[i]);
                scheduledTeams.Add(league.Teams[opponentIndex]);
                Game newGame = new Game(league.Teams[i], league.Teams[opponentIndex]);
                weekSchedule.Add(newGame);
            }

            return weekSchedule;
        }

        private void DisplayWeekSchedule(int weekNumber)
        {
            foreach (var game in regularSeason[weekNumber-1]) Console.WriteLine(game.GetMatchupString(true));
        }

        private void SimulateWeek(int weekNumber)
        {
                Console.Clear();
                Console.WriteLine("--------------\n"
                                + "WEEK " + weekNumber + " RESULTS\n"
                                + "--------------\n");

                foreach (var game in regularSeason[weekNumber - 1]) game.Simulate(false);

                Console.WriteLine("\nPress ENTER to Continue\n");

                var input = Console.ReadLine();
        }
        
        private void CurrentStandings()
        {
            Console.Clear();
            Console.WriteLine("-----------------\n"
                            + "CURRENT STANDINGS\n"
                            + "-----------------\n");

            foreach (var division in league.Nfc.Divisions)
            {
                division.SortDivision();
                Console.WriteLine(division.GetStandings());
            }

            foreach (var division in league.Afc.Divisions)
            {
                division.SortDivision();
                Console.WriteLine(division.GetStandings());
            }

            Console.WriteLine("\nPress ENTER to Continue\n");

            var input = Console.ReadLine();
        }

        private void DisplayPlayoffsBracket()
        {
            Console.Clear();
            Console.WriteLine("--------\n"
                            + "PLAYOFFS\n"
                            + "--------\n");

            Console.WriteLine(league.Nfc.GetPlayoffsSeedingString());
            Console.WriteLine(league.Afc.GetPlayoffsSeedingString());


            Console.WriteLine("\nPress ENTER to Continue\n");

            var input = Console.ReadLine();

        }

        private void PlayoffsWildcard()
        {
            Console.Clear();
            Console.WriteLine("--------------\n"
                            + "WILDCARD ROUND\n"
                            + "--------------\n");

            //offset used for human-readability
            int offset = 1;
            List<Game> NfcWildcardGames = new List<Game> {
                new Game(league.Nfc.playoffSeeds[7 - offset], league.Nfc.playoffSeeds[2 - offset]),
                new Game(league.Nfc.playoffSeeds[6 - offset], league.Nfc.playoffSeeds[3 - offset]),
                new Game(league.Nfc.playoffSeeds[5 - offset], league.Nfc.playoffSeeds[4 - offset])
            };

            List<Game> AfcWildcardGames = new List<Game> {
                new Game(league.Afc.playoffSeeds[7 - offset], league.Afc.playoffSeeds[2 - offset]),
                new Game(league.Afc.playoffSeeds[6 - offset], league.Afc.playoffSeeds[3 - offset]),
                new Game(league.Afc.playoffSeeds[5 - offset], league.Afc.playoffSeeds[4 - offset])
            };

            //Display Matchups
            Console.WriteLine("NFC");
            foreach (Game game in NfcWildcardGames) Console.WriteLine(game.GetMatchupString(false));
            Console.WriteLine("\nAFC");
            foreach (Game game in AfcWildcardGames) Console.WriteLine(game.GetMatchupString(false));

            Console.WriteLine("\nPress ENTER to Continue\n");

            var input = Console.ReadLine();

            //Simulate games and show results
            Console.Clear();
            Console.WriteLine("----------------------\n"
                            + "WILDCARD ROUND RESULTS\n"
                            + "----------------------\n");

            Console.WriteLine("NFC");
            foreach (var game in NfcWildcardGames) game.Simulate(true);
            Console.WriteLine("\nAFC");
            foreach (var game in AfcWildcardGames) game.Simulate(true);

            Console.WriteLine("\nPress ENTER to Continue\n");

            input = Console.ReadLine();
        }

        private void PlayoffsDivisional()
        {
            Console.Clear();
            Console.WriteLine("----------------\n"
                            + "DIVISIONAL ROUND\n"
                            + "----------------\n");

            //NFC Setup
            Team seedOneOpponent = new Team("EMPTY");
            Team matchupTwoTeamOne = new Team("EMPTY");
            Team matchupTwoTeamTwo = new Team("EMPTY");

            for (int i = league.Nfc.playoffSeeds.Count - 1; i >= 0; i--)
            {
                if (league.Nfc.playoffSeeds[i].LostPlayoffs) continue;
                seedOneOpponent = league.Nfc.playoffSeeds[i];
                break;
            }

            for (int i = league.Nfc.playoffSeeds.Count - 1; i >= 0; i--)
            {
                if (league.Nfc.playoffSeeds[i].LostPlayoffs || league.Nfc.playoffSeeds[i] == seedOneOpponent) continue;
                if (matchupTwoTeamOne.Name == "EMPTY")
                {
                    matchupTwoTeamOne = league.Nfc.playoffSeeds[i];
                    continue;
                }
                matchupTwoTeamTwo = league.Nfc.playoffSeeds[i];
                break;
            }

            //offset used for human-readability
            int offset = 1;

            List<Game> NfcDivisionalGames = new List<Game> {
                new Game(league.Nfc.playoffSeeds[1 - offset], seedOneOpponent),
                new Game(matchupTwoTeamOne, matchupTwoTeamTwo)
            };

            //AFC Setup
            seedOneOpponent = new Team("EMPTY");
            matchupTwoTeamOne = new Team("EMPTY");
            matchupTwoTeamTwo = new Team("EMPTY");

            for (int i = league.Afc.playoffSeeds.Count - 1; i >= 0; i--)
            {
                if (league.Afc.playoffSeeds[i].LostPlayoffs) continue;
                seedOneOpponent = league.Afc.playoffSeeds[i];
                break;
            }

            for (int i = league.Afc.playoffSeeds.Count - 1; i >= 0; i--)
            {
                if (league.Afc.playoffSeeds[i].LostPlayoffs || league.Afc.playoffSeeds[i] == seedOneOpponent) continue;
                if (matchupTwoTeamOne.Name == "EMPTY")
                {
                    matchupTwoTeamOne = league.Afc.playoffSeeds[i];
                    continue;
                }
                matchupTwoTeamTwo = league.Afc.playoffSeeds[i];
                break;
            }

            List<Game> AfcDivisionalGames = new List<Game> {
                new Game(league.Afc.playoffSeeds[1 - offset], seedOneOpponent),
                new Game(matchupTwoTeamOne, matchupTwoTeamTwo)
            };

            //Display Matchups
            Console.WriteLine("NFC");
            foreach (Game game in NfcDivisionalGames) Console.WriteLine(game.GetMatchupString(false));
            Console.WriteLine("\nAFC");
            foreach (Game game in AfcDivisionalGames) Console.WriteLine(game.GetMatchupString(false));

            Console.WriteLine("\nPress ENTER to Continue\n");

            var input = Console.ReadLine();

            //Simulate games and show results
            Console.Clear();
            Console.WriteLine("------------------------\n"
                            + "DIVISIONAL ROUND RESULTS\n"
                            + "------------------------\n");

            Console.WriteLine("NFC");
            foreach (var game in NfcDivisionalGames) game.Simulate(true);
            Console.WriteLine("\nAFC");
            foreach (var game in AfcDivisionalGames) game.Simulate(true);

            Console.WriteLine("\nPress ENTER to Continue\n");

            input = Console.ReadLine();
        }

        private void PlayoffsConference()
        {
            Console.Clear();
            Console.WriteLine("----------------\n"
                            + "CONFERENCE ROUND\n"
                            + "----------------\n");

            //NFC Setup
            Team teamOne = new Team("EMPTY");
            Team teamTwo = new Team("EMPTY");

            for (int i = league.Nfc.playoffSeeds.Count - 1; i >= 0; i--)
            {
                if (league.Nfc.playoffSeeds[i].LostPlayoffs) continue;
                if (teamOne.Name == "EMPTY")
                {
                    teamOne = league.Nfc.playoffSeeds[i];
                    continue;
                }
                teamTwo = league.Nfc.playoffSeeds[i];
                break;
            }


            Game NfcChampionship = new Game(teamOne, teamTwo);

            //AFC Setup
            teamOne = new Team("EMPTY");
            teamTwo = new Team("EMPTY");

            for (int i = league.Afc.playoffSeeds.Count - 1; i >= 0; i--)
            {
                if (league.Afc.playoffSeeds[i].LostPlayoffs) continue;
                if (teamOne.Name == "EMPTY")
                {
                    teamOne = league.Afc.playoffSeeds[i];
                    continue;
                }
                teamTwo = league.Afc.playoffSeeds[i];
                break;
            }

            Game AfcChampionship = new Game(teamOne, teamTwo);

            //Display Matchups
            Console.WriteLine("NFC Championship");
            Console.WriteLine(NfcChampionship.GetMatchupString(false));
            Console.WriteLine("\nAFC Championship");
            Console.WriteLine(AfcChampionship.GetMatchupString(false));

            Console.WriteLine("\nPress ENTER to Continue\n");

            var input = Console.ReadLine();

            //Simulate games and show results
            Console.Clear();
            Console.WriteLine("------------------------\n"
                            + "CONFERENCE ROUND RESULTS\n"
                            + "------------------------\n");

            Console.WriteLine("NFC Championship");
            NfcChampionship.Simulate(true);
            Console.WriteLine("\nAFC Championship");
            AfcChampionship.Simulate(true);

            Console.WriteLine("\nPress ENTER to Continue\n");

            input = Console.ReadLine();
        }

        private void Superbowl()
        {
            Console.Clear();
            Console.WriteLine("---------\n"
                            + "SUPERBOWL\n"
                            + "---------\n");

            //Setup
            Team nfcChamp = new Team("EMPTY");
            Team afcChamp = new Team("EMPTY");

            for (int i = league.Nfc.playoffSeeds.Count - 1; i >= 0; i--)
            {
                if (league.Nfc.playoffSeeds[i].LostPlayoffs) continue;
                nfcChamp = league.Nfc.playoffSeeds[i];
                break;
            }

            for (int i = league.Afc.playoffSeeds.Count - 1; i >= 0; i--)
            {
                if (league.Afc.playoffSeeds[i].LostPlayoffs) continue;
                afcChamp = league.Afc.playoffSeeds[i];
                break;
            }

            Game superbowl = new Game(nfcChamp, afcChamp);

            //Display Matchups
            Console.WriteLine("Superbowl");
            Console.WriteLine(superbowl.GetMatchupString(false));

            Console.WriteLine("\nPress ENTER to Continue\n");

            var input = Console.ReadLine();

            //Simulate games and show results
            Console.Clear();
            Console.WriteLine("-----------------\n"
                            + "SUPERBOWL RESULTS\n"
                            + "-----------------\n");

            Console.WriteLine("Superbowl");
            Team winner = superbowl.Simulate(true);

            Console.WriteLine("\n\n" + winner.Name + " are the CHAMPIONS!!");

            Console.WriteLine("\nPress ENTER to Continue\n");

            input = Console.ReadLine();
        }

    }
}
