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

        public Game(Team awayTeam, Team homeTeam)
        {
            AwayTeam = awayTeam;
            HomeTeam = homeTeam;
        }

        public void Simulate()
        {

        }
    }
}
