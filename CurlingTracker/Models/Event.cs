using System;
using System.Collections.Generic;
namespace CurlingTracker.Models
{
    public class Event 
    {
        public Event(string name, DateTime startDate, DateTime endDate, string location, EventType type, List<Draw> draws)
        {
            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Location = location;
            this.Type = type;
            this.EventId = new Guid();
            this.Draws = draws;
        }
        public Guid EventId {get; set;}

        public string Name {get;set;}
        
        public DateTime StartDate {get;set;}

        public DateTime EndDate {get;set;}   

        public string Location {get;set;}     

        public EventType Type {get;set;}

        public List<Draw> Draws {get;set;}
    }
}