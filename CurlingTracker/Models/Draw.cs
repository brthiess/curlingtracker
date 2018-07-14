using System;
using System.Collections.Generic;

namespace CurlingTracker.Models
{
    public class Draw 
    {
        public Draw(DateTime date, string displayName, List<Game> games, Guid eventId)
        {
            this.Date = date;
            this.DisplayName = displayName;
            this.Games = games;
            this.EventId = eventId;
            this.DrawId = Guid.NewGuid();
        }
        public Guid DrawId {get; set;}

        public DateTime Date {get;set;}

        public string DisplayName {get;set;}

        public List<Game> Games{get;set;}

        public Guid EventId {get;set;}
    }
}