using System;
using System.Collections.Generic;

namespace CurlingTracker.Models
{
    public class Team 
    {
        public Team(EventType.TeamType teamType, List<Player> players)
        {
            this.TeamType= teamType;

        }
        public EventType.TeamType TeamType {get;set;}

        public Guid TeamId {get; set;}
        
        public Gender gender{get;set;}

        public List<Player> Players {get;set;}
      
    }
}