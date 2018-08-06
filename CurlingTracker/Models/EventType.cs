using System;
using CurlingTracker.Utility;

namespace CurlingTracker.Models
{
    public class EventType
    {
        public Guid EventTypeId {get;set;}

        public Guid EventId {get;set;}

        public TeamType teamType {get;set;}

        public int NumberOfPlayers {get;set;}

        public Gender Gender {get;set;}

        public int NumberOfEnds {get;set;}

        public enum TeamType {MEN, WOMEN, MIXED_DOUBLES, MIXED}
        
        public EventType(){}
        public EventType(TeamType teamType, int numberOfEnds, Guid eventId)
        {
            this.teamType = teamType;
            this.NumberOfPlayers = GetNumberOfPlayersFromTeamType(teamType);
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
        
        public static int GetNumberOfPlayersFromTeamType(TeamType teamType)
        {
            int numberOfPlayers = 4;
            if (teamType == TeamType.MIXED_DOUBLES)
            {
                numberOfPlayers = 2;
            }
            return   numberOfPlayers;
        }
    }
}