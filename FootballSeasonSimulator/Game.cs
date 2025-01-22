using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSeasonSimulator
{
    internal class Game
    {
        public Team AwayTeam { get; }
        public Team HomeTeam { get; }
        public int AwayTeamScore { get; set; }
        public int HomeTeamScore { get; set; }

        public Game(Team awayTeam, Team homeTeam)
        {
            AwayTeam = awayTeam;
            HomeTeam = homeTeam;
            AwayTeamScore = 0;
            HomeTeamScore = 0;
        }

        public void Simulate()
        {
            AwayTeamScore = GenerateRandomScore();
            HomeTeamScore = GenerateRandomScore();

            AwayTeam.GameResults.Add(new GameResult(HomeTeam, AwayTeamScore, HomeTeamScore));
            HomeTeam.GameResults.Add(new GameResult(AwayTeam, HomeTeamScore, AwayTeamScore));

            Console.WriteLine(AwayTeam.Name + " - " + AwayTeamScore + " : " 
                + HomeTeamScore + " - " + HomeTeam.Name);
        }

        public int GenerateRandomScore()
        {
            int score = 0;
            Random random = new Random();
            int[] scoreOptions = { 0, 0, 0, 0,
                                   3, 3, 3, 3, 3, 
                                   7, 7, 7, 7, 
                                   6, 6, 
                                   2 
                                 };

            for (int i = 0; i < 6; i++)
            {
                int scoreIndex = random.Next(scoreOptions.Length);
                score += scoreOptions[scoreIndex];
            }

            return score;
        }
    }
}
