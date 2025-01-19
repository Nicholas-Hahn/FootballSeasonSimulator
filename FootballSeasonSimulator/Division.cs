using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSeasonSimulator
{
    internal class Division
    {
        public string name { get; }
        public List<Team> teams { get; set; }

        public Division(string name)
        {
            this.name = name;
            teams = new List<Team>();
        }
    }
}
