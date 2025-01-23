using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FootballSeasonSimulator
{
    internal class Playoffs
    {
        public List<Team> AfcTeams { get; }
        public List<Team> NfcTeams { get; }

        public Playoffs(List<Team> nfcTeams, List<Team> afcTeams)
        {
            NfcTeams = nfcTeams;
            AfcTeams = afcTeams;
        }

        public void Simulate()
        {
            DisplayPlayoffsTeams();
            PlayoffsWildcard();
            PlayoffsDivisional();
            PlayoffsConference();
            Superbowl();
        }

        public string GetPlayoffsSeedingString(List<Team> playoffSeeds)
        {
            string output = "";

            for (int i = 0; i < playoffSeeds.Count; i++)
            {
                output += (i + 1) + " - " + playoffSeeds[i].Name + playoffSeeds[i].GetRecordString() + "\n";
            }

            return output;
        }

        private void DisplayPlayoffsTeams()
        {
            Console.Clear();
            Console.WriteLine("--------\n"
                            + "PLAYOFFS\n"
                            + "--------\n");

            Console.WriteLine("NFC");
            Console.WriteLine(GetPlayoffsSeedingString(NfcTeams));
            Console.WriteLine("\nAFC");
            Console.WriteLine(GetPlayoffsSeedingString(AfcTeams));


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
                new Game(NfcTeams[7 - offset], NfcTeams[2 - offset]),
                new Game(NfcTeams[6 - offset], NfcTeams[3 - offset]),
                new Game(NfcTeams[5 - offset], NfcTeams[4 - offset])
            };

            List<Game> AfcWildcardGames = new List<Game> {
                new Game(AfcTeams[7 - offset], AfcTeams[2 - offset]),
                new Game(AfcTeams[6 - offset], AfcTeams[3 - offset]),
                new Game(AfcTeams[5 - offset], AfcTeams[4 - offset])
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

            for (int i = NfcTeams.Count - 1; i >= 0; i--)
            {
                if (NfcTeams[i].LostPlayoffs) continue;
                seedOneOpponent = NfcTeams[i];
                break;
            }

            for (int i = NfcTeams.Count - 1; i >= 0; i--)
            {
                if (NfcTeams[i].LostPlayoffs || NfcTeams[i] == seedOneOpponent) continue;
                if (matchupTwoTeamOne.Name == "EMPTY")
                {
                    matchupTwoTeamOne = NfcTeams[i];
                    continue;
                }
                matchupTwoTeamTwo = NfcTeams[i];
                break;
            }

            //offset used for human-readability
            int offset = 1;

            List<Game> NfcDivisionalGames = new List<Game> {
                new Game(NfcTeams[1 - offset], seedOneOpponent),
                new Game(matchupTwoTeamOne, matchupTwoTeamTwo)
            };

            //AFC Setup
            seedOneOpponent = new Team("EMPTY");
            matchupTwoTeamOne = new Team("EMPTY");
            matchupTwoTeamTwo = new Team("EMPTY");

            for (int i = AfcTeams.Count - 1; i >= 0; i--)
            {
                if (AfcTeams[i].LostPlayoffs) continue;
                seedOneOpponent = AfcTeams[i];
                break;
            }

            for (int i = AfcTeams.Count - 1; i >= 0; i--)
            {
                if (AfcTeams[i].LostPlayoffs || AfcTeams[i] == seedOneOpponent) continue;
                if (matchupTwoTeamOne.Name == "EMPTY")
                {
                    matchupTwoTeamOne = AfcTeams[i];
                    continue;
                }
                matchupTwoTeamTwo = AfcTeams[i];
                break;
            }

            List<Game> AfcDivisionalGames = new List<Game> {
                new Game(AfcTeams[1 - offset], seedOneOpponent),
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

            for (int i = NfcTeams.Count - 1; i >= 0; i--)
            {
                if (NfcTeams[i].LostPlayoffs) continue;
                if (teamOne.Name == "EMPTY")
                {
                    teamOne = NfcTeams[i];
                    continue;
                }
                teamTwo = NfcTeams[i];
                break;
            }


            Game NfcChampionship = new Game(teamOne, teamTwo);

            //AFC Setup
            teamOne = new Team("EMPTY");
            teamTwo = new Team("EMPTY");

            for (int i = AfcTeams.Count - 1; i >= 0; i--)
            {
                if (AfcTeams[i].LostPlayoffs) continue;
                if (teamOne.Name == "EMPTY")
                {
                    teamOne = AfcTeams[i];
                    continue;
                }
                teamTwo = AfcTeams[i];
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

            for (int i = NfcTeams.Count - 1; i >= 0; i--)
            {
                if (NfcTeams[i].LostPlayoffs) continue;
                nfcChamp = NfcTeams[i];
                break;
            }

            for (int i = AfcTeams.Count - 1; i >= 0; i--)
            {
                if (AfcTeams[i].LostPlayoffs) continue;
                afcChamp = AfcTeams[i];
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
