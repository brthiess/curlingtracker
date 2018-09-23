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

        public Standings Standings {get;set;}

        public List<Bracket> Brackets {get;set;}

        public Playoff Playoff {get;set;}

        public Event() { }
        public Event(string name, DateTime startDate, DateTime endDate, string location, EventType type, List<Draw> draws = null, string czId = null, Guid? eventId = null, Standings standings = null, List<Bracket> brackets = null, Playoff playoff = null)
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
            this.Standings = standings;
            this.Brackets = brackets;
            this.Playoff = playoff;
        }


        public List<Game> GetCurrentGames()
        {
            return this.CurrentDraw.Games;
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
                    if ((timeBetween >= 0 && timeBetween < minTimeBetween) || (timeBetween < 0 && timeBetween > -1))
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

        public string GetEventFormatToString()
        {
            if (this.Type.EventFormat == EventFormat.KNOCKOUT)
            {
                return "Bracket";
            }
            else 
            {
                return "Standings";
            }
        }

        public bool IsRoundRobin()
        {
            if (this.Type.EventFormat == EventFormat.ROUND_ROBIN)
            {
                return true;
            }
            return false;
        }

        public bool IsKnockout()
        {
            if (this.Type.EventFormat == EventFormat.KNOCKOUT)
            {
                return true;
            }
            return false;
        }

        public string GetStandingsHtml()
        {
            return this.Standings.Html;    
        }

        public List<Bracket> GetBrackets()
        {
            return this.Brackets;
        }

        public Playoff GetPlayoff()
        {
            return this.Playoff;
        }
        public string GetUrl()
        {
            return "/events/" + this.Url + "/";
        }

        public string GetQueryString()
        {
            return "eventId=" + this.EventId.ToString();
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