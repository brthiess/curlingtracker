using System;

namespace CurlingTracker.Models
{
    public class Game
    {
        public Game(){
            Team t = new Team();
            t.Type = Team.TeamType.MIXED_CLASSIC;
        }
        public Guid Id {get; set;}

        public Guid DrawId{get; set;}

        public Guid EventId{get; set;}

        public Guid Team1Id{get; set;}

        public Guid Team2Id{get; set;}
                
    }
}