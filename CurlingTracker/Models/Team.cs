using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CurlingTracker.Models
{
    public class Team : IPrintable
    {
        public string TeamId {get; set;}
        
        public EventType.TeamType TeamType {get;set;}
        
        public Gender gender{get;set;}

        
        public List<Player> Players {get;set;}


        public string Name {get;set;}

        public Team(){}        
        public Team(EventType.TeamType teamType, List<Player> players, string name = null)
        {
            this.TeamType= teamType;
            bool foundSkip = false;
            foreach(Player player in players)
            {
                if (player.IsSkip)
                {
                    foundSkip = true;
                }
            }
            if (!foundSkip && teamType != EventType.TeamType.MIXED_DOUBLES)
            {
                throw new Exception("Created team without a skip!");
            }
            this.Players = players;
            this.Name = GetTeamName(name, teamType);
            this.TeamId = GetTeamId();
        }

        public List<Player> GetPlayersSorted()
        {
            var players =  Players.OrderBy(p=> (int) p.position).ToList();
            return players;
        }

        private string GetTeamName(string name, EventType.TeamType teamType)
        {   
            string teamName = "";

            teamName = name;
            if (teamType == EventType.TeamType.MIXED_DOUBLES)
            {
                teamName = GetMixedDoublesTeamShortNameFromTeam();
                if (string.IsNullOrEmpty(teamName))
                {
                    teamName = name;
                }
            }
            if (string.IsNullOrEmpty(teamName))
            {
                teamName = GetTeamShortName();
            }  
            if (string.IsNullOrEmpty(teamName))
            {
                teamName = "Unknown";
            }
            return teamName;                
        }

        public string GetTeamShortName()
        {
            if (this.TeamType == EventType.TeamType.MIXED_DOUBLES)
            {
                return GetMixedDoublesTeamShortNameFromTeam();
            }
            else
            {
                return GetClassicTeamShortNameFromTeam();
            }
        }
        
        private string GetClassicTeamShortNameFromTeam()
        {
            string teamName = "";
            foreach(Player player in this.Players)
            {
                if (player.IsSkip)
                {
                    teamName += player.LastName;
                }
            }
            return teamName;
        }
        private string GetMixedDoublesTeamShortNameFromTeam()
        {
            string teamName = "";
            int i = 0;
            foreach(Player player in this.Players)
            {
                teamName += player.LastName;
                if (i == 0)
                {
                    teamName += "/";
                }
                i++;
            }
            return teamName;
        }

        public string Print()
        {
            string resultString = "Team: " + this.GetTeamShortName() + "\n";
            foreach(Player p in this.Players)
            {
                resultString += p.Print() + "\n";
            }
            return resultString;
        }
        private string GetTeamId()
        {
            string teamId = "";
            foreach(Player p in this.Players)
            {
                teamId += p.FirstName + p.LastName;
            }
            teamId += this.gender.ToString();
            teamId += this.TeamType.ToString();
            teamId = Utility.StringUtil.CreateMD5(teamId);
            return teamId;
        }
    }
}