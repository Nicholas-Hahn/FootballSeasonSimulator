using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSeasonSimulator
{
    internal class Team
    {
        public string Name { get; }
        public int OffensePower { get; }
        public int DefensePower { get; }
        public List<GameResult> GameResults { get; }

        public Team(string name, int offensePower, int defensePower)
        {
            Name = name;
            OffensePower = offensePower;
            DefensePower = defensePower;
            GameResults = new List<GameResult>();
        }

        public Team(string name)
        {
            Name = name;
            OffensePower = 100;
            DefensePower = 100;
            GameResults = new List<GameResult>();
        }

        public string GetRecordString()
        {
            if (GameResults.Count == 0) return "";

            int wins = 0;
            int losses = 0;
            int ties = 0;

            foreach (GameResult result in GameResults)
            {
                if (result.OpponentScore > result.Score) losses++;
                if (result.OpponentScore < result.Score) wins++;
                if (result.OpponentScore == result.Score) ties++;
            }

            string record = wins + "-" + losses;
            if (ties > 0) record += "-" + ties;

            return " (" + record + ")";
        }
    }
}
