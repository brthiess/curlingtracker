using System;

namespace CurlingTracker.Models
{
    public class Draw 
    {
        public Draw(DateTime date, string displayName)
        {
            this.Date = date;
            this.DisplayName = displayName;
        }
        public Guid DrawId {get; set;}

        public DateTime Date {get;set;}

        public string DisplayName {get;set;}
    }
}