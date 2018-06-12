using System;

namespace CurlingTracker.Models
{
    public class Draw 
    {
        public Draw(DateTime date)
        {
            this.Date = date;
        }
        public Guid DrawId {get; set;}

        public DateTime Date {get;set;}

        
    }
}