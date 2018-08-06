using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurlingTracker.Models
{
    public class Draw 
    {
        public Guid DrawId {get; set;}

        [Required]
        public DateTime Date {get;set;}

        [Required]
        public string DisplayName {get;set;}

        public List<Game> Games{get;set;}

        [Required]
        public Guid EventId {get;set;}
        
        public bool IsOver {get;set;}

        public Draw(){}
        public Draw(DateTime date, string displayName, List<Game> games, Guid eventId, bool isOver = false)
        {
            this.Date = date;
            this.DisplayName = displayName;
            this.Games = games;
            this.EventId = eventId;
            this.DrawId = Guid.NewGuid();
            this.IsOver = isOver;
        }

        
    }
}