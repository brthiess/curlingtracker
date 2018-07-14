using System;
using CurlingTracker.Utility;

namespace CurlingTracker.Models
{
    public class EventType
    {
        public Guid EventTypeId;

        public Guid EventId;

        public TeamType teamType {get;set;}

        public int NumberOfPlayers {get;set;}

        public Gender Gender {get;set;}

        public int NumberOfEnds {get;set;}

        public enum TeamType {MEN, WOMEN, MIXED_DOUBLES, MIXED}
        
        public EventType(TeamType teamType, int numberOfEnds, Guid eventId)
        {
            this.teamType = teamType;
            this.NumberOfPlayers = 4;
            if (teamType == TeamType.MIXED_DOUBLES)
            {
                this.NumberOfPlayers = 2;
            }
            this.Gender = GetGenderFromTeamType(this.teamType);
            this.NumberOfEnds = numberOfEnds;
            this.EventTypeId = Guid.NewGuid();
            this.EventId = eventId;
        }

        private Gender GetGenderFromTeamType(TeamType teamType)
        {
            if (teamType == TeamType.MEN)
            {
                return Gender.Male;
            }
            else if (teamType == TeamType.WOMEN)
            {
                return Gender.Female;
            }
            else
            {
                return Gender.Mixed;
            }
        }

        public string GetTeamTypeToString()
        {
            return StringUtil.FirstLetterToUpper(teamType.ToString().Replace("_", " "));
        }
    }
}