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

            //TODO playoffs setup
            //TODO playoffs simulation
        }
    }
}