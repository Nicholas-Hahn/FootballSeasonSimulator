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
        public int AwayTeamScore { get; }
        public int HomeTeamScore { get; }

        public Game(Team awayTeam, Team homeTeam)
        {
            AwayTeam = awayTeam;
            HomeTeam = homeTeam;
            AwayTeamScore = 0;
            HomeTeamScore = 0;
        }

        public void Simulate()
        {
            //TODO Get score of game
            //TODO create results for each team, add to team results
            Console.WriteLine(AwayTeam.Name + " - " + AwayTeamScore + " : " 
                + HomeTeamScore + " - " + HomeTeam.Name);
        }
    }
}
