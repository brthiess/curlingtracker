using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace CurlingTracker.Models
{
    public class Event : IPrintable
    {
        [Required]
        public Guid EventId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Location { get; set; }

        public EventType Type { get; set; }

        public List<Draw> Draws { get; set; }

        public string CZId { get; set; }

        [Required]
        public string Url { get; set; }

        public bool IsOverAndFullyParsed { get; set; }

        public Event() { }
        public Event(string name, DateTime startDate, DateTime endDate, string location, EventType type, List<Draw> draws = null, string czId = null, Guid? eventId = null)
        {
            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Location = location;
            this.Type = type;
            this.EventId = (eventId.HasValue ? eventId.Value : Guid.NewGuid());
            this.Url = GenerateUrl();
            this.Draws = draws;
            this.CZId = czId;
            this.IsOverAndFullyParsed = false;
        }


        private Draw _currentDraw = null;
        public Draw CurrentDraw
        {
            get
            {
                if (_currentDraw != null)
                {
                    return _currentDraw;
                }

                double minTimeBetween = double.MaxValue;
                Draw currentDraw = null;
                foreach (Draw d in this.Draws)
                {
                    double timeBetween = (DateTime.Now - d.Date.ToLocalTime()).TotalMinutes;
                    if ((timeBetween >= 0 && timeBetween < minTimeBetween) || (timeBetween < 0 && timeBetween > -15))
                    {
                        currentDraw = d;
                        minTimeBetween = timeBetween;
                    }
                }
                _currentDraw = currentDraw;
                return currentDraw;
            }
        }

        public Draw GetDraw(string drawId)
        {
            foreach(Draw d in this.Draws)
            {
                if (d.DrawId.ToString() == drawId)
                {
                    return d;
                }
            }
            return null;
        }

        public string GetUrl()
        {
            return "/events/" + this.Url + "/";
        }

        private string GenerateUrl()
        {
            string url = "";
            url = Utility.StringUtil.ConvertToUrl(this.Name);
            url = this.StartDate.Year.ToString() + "-" + url + "-" + Utility.StringUtil.ConvertToUrl(this.Type.Gender.ToString());
            return url;
        }
        public string Print()
        {
            string resultString = "Name: " + this.Name + "\n";
            resultString += "Url: " + this.Url;
            resultString += "Type: " + this.Type + "\n";
            resultString += "Location: " + this.Location + "\n";
            resultString += "Date: " + this.StartDate.ToString() + "\n";
            resultString += "CZID: " + this.CZId + "\n";
            resultString += "IsOverAndFullyParsed: " + this.IsOverAndFullyParsed.ToString();
            return resultString;
        }
    }
}