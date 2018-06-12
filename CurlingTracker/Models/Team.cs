using System;

namespace CurlingTracker.Models
{
    public class Team 
    {
        public enum TeamType {MIXED_DOUBLES, CLASSIC, MIXED_CLASSIC}

        public Guid TeamId {get; set;}

        public TeamType Type {get;set;}
        
        public enum Gender {MEN, WOMEN}
        public DateTime StartDate {get;set;}

        public DateTime EndDate {get;set;}        
    }
}