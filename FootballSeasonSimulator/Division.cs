﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballSeasonSimulator
{
    internal class Division
    {
        public string Name { get; }
        public List<Team> Teams { get; set; }

        public Division(string name)
        {
            Name = name;
            Teams = new List<Team>();
        }
    }
}
