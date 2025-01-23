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
        public List<Team> playoffSeeds { get; }

        public Conference(string name)
        {
            Name = name;
            Divisions = new List<Division>();
            playoffSeeds = new List<Team>();
        }

        public void FillPlayoffSeeds()
        {
            //Get all Divisonal winners and all other teams
            List<Team> divisionWinners = new List<Team>();
            List<Team> remainingTeams = new List<Team>();

            foreach (Division division in Divisions) 
            {
                divisionWinners.Add(division.Teams[0]);

                remainingTeams.Add(division.Teams[1]);
                remainingTeams.Add(division.Teams[2]);
                remainingTeams.Add(division.Teams[3]);
            }

            //sort divisional winners by wins
            List<Team> sortedDivisionWinners = new List<Team>();
            sortedDivisionWinners.Add(divisionWinners[0]);

            for (int i = 1; i < divisionWinners.Count; i++)
            {
                for (int j = 0; j < sortedDivisionWinners.Count; j++)
                {
                    if (divisionWinners[i].GetWins() > sortedDivisionWinners[j].GetWins())
                    {
                        sortedDivisionWinners.Insert(j, divisionWinners[i]);
                        break;
                    }
                }
                if (!sortedDivisionWinners.Contains(divisionWinners[i])) sortedDivisionWinners.Add(divisionWinners[i]);
            }

            //sort remaining teams by wins
            List<Team> sortedRemainingTeams = new List<Team>();
            sortedRemainingTeams.Add(remainingTeams[0]);

            for (int i = 1; i < remainingTeams.Count; i++)
            {
                for (int j = 0; j < sortedRemainingTeams.Count; j++)
                {
                    if (remainingTeams[i].GetWins() > sortedRemainingTeams[j].GetWins())
                    {
                        sortedRemainingTeams.Insert(j, remainingTeams[i]);
                        break;
                    }
                }
                if (!sortedRemainingTeams.Contains(remainingTeams[i])) sortedRemainingTeams.Add(remainingTeams[i]);
            }

            //Fill all seeds based on divisional winners and top 3 remaining for wildcards
            for (int i = 0;i < sortedDivisionWinners.Count; i++)
            {
                playoffSeeds.Add(sortedDivisionWinners[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                playoffSeeds.Add(sortedRemainingTeams[i]);
            }
        }

        public string GetPlayoffsSeedingString()
        {
            string output = Name + "\n";
            for (int i = 0; i < playoffSeeds.Count; i++)
            {
                output += (i+1) + " - " + playoffSeeds[i].Name + playoffSeeds[i].GetRecordString() + "\n";
            }

            return output;
        }
    }
}
