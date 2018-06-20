using System;

namespace CurlingTracker.Models
{
    public class EventType
    {
        public enum TeamType {MEN, WOMEN, MIXED_DOUBLES, MIXED}
        public EventType(TeamType teamType)
        {
            this.teamType = teamType;
            this.NumberOfPlayers = 4;
            if (teamType == TeamType.MIXED_DOUBLES)
            {
                this.NumberOfPlayers = 2;
            }
        }
        public TeamType teamType {get;set;}

        public int NumberOfPlayers {get;set;}
    }
}