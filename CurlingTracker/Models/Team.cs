using System;
using System.Collections.Generic;

namespace CurlingTracker.Models
{
    public class Team 
    {
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
        }
        public EventType.TeamType TeamType {get;set;}

        public Guid TeamId {get; set;}
        
        public Gender gender{get;set;}

        public List<Player> Players {get;set;}
      
    }
}