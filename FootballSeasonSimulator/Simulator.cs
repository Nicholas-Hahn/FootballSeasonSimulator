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

            List<Team> nfcPlayoffSeeds = league.Nfc.GeneratePlayoffSeeds();
            List<Team> afcPlayoffSeeds = league.Afc.GeneratePlayoffSeeds();

            Playoffs playoffs = new Playoffs(nfcPlayoffSeeds, afcPlayoffSeeds);

            playoffs.Simulate();
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
    }
}
