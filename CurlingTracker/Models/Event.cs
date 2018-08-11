using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CurlingTracker.Models
{
    public class Event 
    {
        [Required]
        public Guid EventId {get; set;}

        [Required]
        public string Name {get;set;}
        
        [Required]
        public DateTime StartDate {get;set;}

        [Required]
        public DateTime EndDate {get;set;}   

        [Required]
        public string Location {get;set;}     

        public EventType Type {get;set;}

        public List<Draw> Draws {get;set;}

        public string CZId {get;set;}
        
        public bool IsOverAndFullyParsed {get; set;}

        public Event(){}
        public Event(string name, DateTime startDate, DateTime endDate, string location, EventType type, List<Draw> draws = null, string czId = null, Guid? eventId = null)
        {
            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Location = location;
            this.Type = type;
            this.EventId = (eventId.HasValue ? eventId.Value : Guid.NewGuid());
            this.Draws = draws;
            this.CZId = czId;
            this.IsOverAndFullyParsed = false;
        }
       

        private Draw _currentDraw = null;
        public Draw CurrentDraw {
            get
            {
                if (_currentDraw != null)
                {
                    return _currentDraw;
                }

                int minTimeBetween = int.MaxValue;
                Draw currentDraw = null;
                foreach(Draw d in this.Draws)
                {
                    int timeBetween = DateTime.Compare(DateTime.Now, d.Date);
                    if (timeBetween >= 0 && timeBetween < minTimeBetween)
                    {
                        currentDraw = d;
                    }
                }
                _currentDraw = currentDraw;
                return currentDraw;
            }
        }
    }
}