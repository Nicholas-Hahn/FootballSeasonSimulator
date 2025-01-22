using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSeasonSimulator
{
    internal struct GameResult
    {
        public Team Opponent { get; }
        public int Score { get; }
        public int OpponentScore { get; }

        public GameResult(Team opponent, int score, int opponentScore)
        {
            Opponent = opponent;
            Score = score;
            OpponentScore = opponentScore;
        }
    }
}
