using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSeasonSimulator
{
    internal class Conference
    {
        public string Name { get; }
        public List<Division> Divisions { get; }   

        public Conference(string name)
        {
            Name = name;
            Divisions = new List<Division>();
        }

    }
}
