using System;
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

        public void SortDivision()
        {
            //TODO implement sorting by team records
            List<Team> sortedTeams = new List<Team>();

            sortedTeams.Add(Teams[0]);

            for (int i = 1; i < Teams.Count; i++)
            {
                for (int j = 0; j < sortedTeams.Count; j++)
                {
                    if (Teams[i].GetWins() > sortedTeams[j].GetWins())
                    {
                        sortedTeams.Insert(j, Teams[i]);
                        break;
                    }
                }
                if(!sortedTeams.Contains(Teams[i])) sortedTeams.Add(Teams[i]);
            }

            Teams = sortedTeams;
        }

        public string GetStandings()
        {
            string standings = Name + "\n";

            foreach (Team team in Teams)
            {
                standings += team.Name + team.GetRecordString() + "\n";
            }

            return standings;
        }
    }
}
