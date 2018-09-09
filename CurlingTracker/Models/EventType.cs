using System;
using CurlingTracker.Utility;

namespace CurlingTracker.Models
{
    public class EventType : IPrintable
    {
        public Guid EventTypeId {get;set;}

        public Guid EventId {get;set;}

        public TeamType teamType {get;set;}

        public EventFormat EventFormat {get;set;}

        public int NumberOfPlayers {get;set;}

        public Gender Gender {get;set;}

        public int NumberOfEnds {get;set;}

        public enum TeamType {MEN, WOMEN, MIXED_DOUBLES, MIXED}
        
        public EventType(){}
        public EventType(TeamType teamType, int numberOfEnds, Guid eventId, EventFormat eventFormat)
        {
            this.teamType = teamType;
            this.NumberOfPlayers = GetNumberOfPlayersFromTeamType(teamType);
            this.Gender = GetGenderFromTeamType(this.teamType);
            this.NumberOfEnds = numberOfEnds;
            this.EventTypeId = Guid.NewGuid();
            this.EventId = eventId;
            this.EventFormat = eventFormat;
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

        public void SetTeamType(TeamType teamType)
        {
            this.teamType = teamType;
            this.NumberOfPlayers = GetNumberOfPlayersFromTeamType(teamType);
            this.Gender = GetGenderFromTeamType(teamType);
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

        public string Print()
        {
            string resultString = "TeamType: " + this.teamType + "\n";
            resultString += "Gender: " + this.Gender.ToString();
            resultString += "Number of Players: " + this.NumberOfPlayers;
            resultString += "Number of Ends: " + this.NumberOfEnds;
            return resultString;
        }
    }
}