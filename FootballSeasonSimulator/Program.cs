using System;
using System.Xml.Serialization;

namespace FootballSeasonSimulator
{
    class Program
    {
        static int lastSelection = 0;
        static League league = new League();

        public static void Main(string[] args)
        {
            league.BuildLeague();
            //Loop();

            //TODO team season generation based on league rules

            //TODO preconfiguration of team powers
            // - offense
            // - defense

            //TODO preconfiguration of last season results for season generation

            //TODO game simulation
            //TODO week simulation
            //TODO playoffs setup
            //TODO playoffs simulation
            //TODO results playout
        }

        static private void Loop()
        {

            MainMenu();

            if (lastSelection == 1)
            {
                //GenerateSeason();
                WeekStart();
                //Week Simulate
                //Team Menu
                //Standings Menu
            }
        }

        static private void MainMenu()
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

        static private void WeekStart()
        {
            lastSelection = 0;
            while (lastSelection == 0)
            {
                Console.Clear();
                Console.WriteLine("------\n"
                                + "WEEK 1\n"
                                + "------\n");

                Console.WriteLine();

                Console.WriteLine("OPTIONS:\n"
                                + "1. Start Season\n"
                                + "9. Quit\n");

                var input = Console.ReadLine();

                if (input != null) int.TryParse(input, out lastSelection);
                if (lastSelection != 1 && lastSelection != 9) lastSelection = 0;
            }
        }
    }
}