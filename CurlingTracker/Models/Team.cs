using System;
using System.Collections.Generic;

namespace CurlingTracker.Models
{
    public class Team 
    {
        public Guid TeamId {get; set;}
        
        public EventType.TeamType TeamType {get;set;}
        
        public Gender gender{get;set;}

        public List<Player> Players {get;set;}

        public string Name {get;set;}

        public Team(){}        
        public Team(EventType.TeamType teamType, List<Player> players)
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
            this.Name = GetTeamShortName();
        }

        private string GetTeamShortName()
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

    }
}