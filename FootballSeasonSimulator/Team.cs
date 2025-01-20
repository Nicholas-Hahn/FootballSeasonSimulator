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
        public List<Game> Games { get; }

        public Team(string name, int offensePower, int defensePower)
        {
            Name = name;
            OffensePower = offensePower;
            DefensePower = defensePower;
            Games = new List<Game>();
        }

        public Team(string name)
        {
            Name = name;
            OffensePower = 100;
            DefensePower = 100;
            Games = new List<Game>();
        }
    }
}
