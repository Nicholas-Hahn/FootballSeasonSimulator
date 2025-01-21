using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSeasonSimulator
{
    internal class Simulator
    {
        int lastSelection = 0;
        League league;
        List<List<Game>> regularSeason;

        public Simulator()
        {
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

            MainMenu();

            if (lastSelection == 1)
            {
                GenerateSeason();
                WeekStart(1);
                //Week Simulate
                //Team Menu
                //Standings Menu
            }
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

                if (input != null) int.TryParse(input, out lastSelection);
                if (lastSelection != 1 && lastSelection != 9) lastSelection = 0;
            }
        }

        private void WeekStart(int weekNumber)
        {
            lastSelection = 0;
            while (lastSelection == 0)
            {
                Console.Clear();
                Console.WriteLine("------\n"
                                + "WEEK " + weekNumber + "\n"
                                + "------\n");

                DisplayWeekSchedule(weekNumber);
                Console.WriteLine();

                Console.WriteLine("OPTIONS:\n"
                                + "1. Simulate Week\n"
                                + "9. Quit\n");

                var input = Console.ReadLine();

                if (input != null) int.TryParse(input, out lastSelection);
                if (lastSelection != 1 && lastSelection != 9) lastSelection = 0;
            }
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
            foreach (var game in regularSeason[weekNumber-1])
            {
                string output = game.AwayTeam.Name + " v " + game.HomeTeam.Name; 

                Console.WriteLine(output);
            }
        }
    }
}
