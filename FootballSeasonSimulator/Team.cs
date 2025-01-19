using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSeasonSimulator
{
    internal class Team
    {
        public int Id { get; }
        public string Name { get; }
        public int OffensePower { get; }
        public int DefensePower { get; }

        public Team(int Id, string Name, int OffensePower, int DefensePower)
        {
            this.Id = Id;
            this.Name = Name;
            this.OffensePower = OffensePower;
            this.DefensePower = DefensePower;
        }

    }
}
