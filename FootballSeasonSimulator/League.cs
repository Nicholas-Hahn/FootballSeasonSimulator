using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FootballSeasonSimulator
{
    internal class League
    {
        public Conference Nfc { get; }
        public Conference Afc { get; }

        public League()
        {
            Nfc = new Conference("NFC");
            Afc = new Conference("AFC");
        }

        public void BuildLeague()
        {
            Division newDiv = new Division(Nfc.Name + "-North");
            newDiv.Teams.Add(new Team("Lions"));
            newDiv.Teams.Add(new Team("Packers"));
            newDiv.Teams.Add(new Team("Bears"));
            newDiv.Teams.Add(new Team("Vikings"));
            Nfc.Divisions.Add(newDiv);
            newDiv = new Division(Nfc.Name + "-South");
            newDiv.Teams.Add(new Team("Buccaneers"));
            newDiv.Teams.Add(new Team("Falcons"));
            newDiv.Teams.Add(new Team("Panthers"));
            newDiv.Teams.Add(new Team("Saints"));
            Nfc.Divisions.Add(newDiv);
            newDiv = new Division(Nfc.Name + "-East");
            newDiv.Teams.Add(new Team("Eagles"));
            newDiv.Teams.Add(new Team("Commanders"));
            newDiv.Teams.Add(new Team("Cowboys"));
            newDiv.Teams.Add(new Team("Giants"));
            Nfc.Divisions.Add(newDiv);
            newDiv = new Division(Nfc.Name + "-West");
            newDiv.Teams.Add(new Team("Rams"));
            newDiv.Teams.Add(new Team("Seahawks"));
            newDiv.Teams.Add(new Team("Cardinals"));
            newDiv.Teams.Add(new Team("49ers"));
            Nfc.Divisions.Add(newDiv);

            newDiv = new Division(Afc.Name + "-North");
            newDiv.Teams.Add(new Team("Ravens"));
            newDiv.Teams.Add(new Team("Steelers"));
            newDiv.Teams.Add(new Team("Bengals"));
            newDiv.Teams.Add(new Team("Browns"));
            Afc.Divisions.Add(newDiv);
            newDiv = new Division(Afc.Name + "-South");
            newDiv.Teams.Add(new Team("Texans"));
            newDiv.Teams.Add(new Team("Colts"));
            newDiv.Teams.Add(new Team("Jaguars"));
            newDiv.Teams.Add(new Team("Titans"));
            Afc.Divisions.Add(newDiv);
            newDiv = new Division(Afc.Name + "-East");
            newDiv.Teams.Add(new Team("Bills"));
            newDiv.Teams.Add(new Team("Dolphins"));
            newDiv.Teams.Add(new Team("Jets"));
            newDiv.Teams.Add(new Team("Patriots"));
            Afc.Divisions.Add(newDiv);
            newDiv = new Division(Afc.Name + "-West");
            newDiv.Teams.Add(new Team("Chiefs"));
            newDiv.Teams.Add(new Team("Chargers"));
            newDiv.Teams.Add(new Team("Broncos"));
            newDiv.Teams.Add(new Team("Raiders"));
            Afc.Divisions.Add(newDiv);

            DebugPrintout();
        }

        private void DebugPrintout()
        {
            Console.WriteLine("League Built");
            Console.WriteLine("NFC");
            foreach (var div in Nfc.Divisions)
            {
                Console.WriteLine(div.Name);
                foreach (var team in div.Teams)
                {
                    Console.WriteLine(team.Name);
                }
            }
            Console.WriteLine("AFC");
            foreach (var div in Afc.Divisions)
            {
                Console.WriteLine(div.Name);
                foreach (var team in div.Teams)
                {
                    Console.WriteLine(team.Name);
                }
            }
        }
    }
}
