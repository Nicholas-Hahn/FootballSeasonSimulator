using System;
using System.Xml.Serialization;

namespace FootballSeasonSimulator
{
    class Program
    {
        public static void Main(string[] args)
        {
            Simulator sim = new Simulator();
            sim.Execute();
            //TODO team season generation based on league rules

            //TODO preconfiguration of team powers
            // - offense
            // - defense

            //TODO preconfiguration of last season results for season generation

            //TODO game simulation
            //TODO week simulation
            //TODO playoffs setup
            //TODO playoffs simulation
            //TODO results playout
        }
    }
}