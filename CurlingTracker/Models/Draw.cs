using System;
using System.Collections.Generic;

namespace CurlingTracker.Models
{
    public class Draw 
    {
        public Draw(DateTime date, string displayName, List<Game> games)
        {
            this.Date = date;
            this.DisplayName = displayName;
            this.Games = games;
        }
        public Guid DrawId {get; set;}

        public DateTime Date {get;set;}

        public string DisplayName {get;set;}

        public List<Game> Games{get;set;}
    }
}