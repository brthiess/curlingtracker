using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurlingTracker.Models
{
    public class Draw : IPrintable
    {
        public Guid DrawId {get; set;}

        [Required]
        public DateTime Date {get;set;}

        [Required]
        public string DisplayName {get;set;}

        public List<Game> Games{get;set;}

        [Required]
        public Guid EventId {get;set;}
        
        public bool IsOverAndFullyParsed {get;set;}

        [Required]
        public string Url {get;set;}
        public Draw(){}
        public Draw(DateTime date, string displayName, List<Game> games, Guid eventId, bool isOverAndFullyParsed, Guid drawId)
        {
            this.Date = date;
            this.DisplayName = displayName;
            this.Games = games;
            this.EventId = eventId;
            this.DrawId = drawId;
            this.IsOverAndFullyParsed = isOverAndFullyParsed;
            this.Url = GenerateUrl();
        }

        public string GetUrl(Event e)
        {
            return e.GetUrl() + "draws/" + this.Url;
        }

        private string GenerateUrl()
        {
            return Utility.StringUtil.ConvertToUrl(this.DisplayName) + "-" + Utility.StringUtil.ConvertToUrl(this.DrawId.ToString().Substring(0,6));
        }
        public string Print()
        {
            string resultString = "Display Name: " + this.DisplayName + "\n";
            resultString += "Event Id: " + this.EventId + "\n";
            resultString += "Draw Id: " + this.DrawId + "\n";
            resultString += "IsOverAndFullyParsed: " + this.IsOverAndFullyParsed.ToString() + "\n";
            resultString += "Date: " + this.Date + "\n";

            return resultString;
        }
    }
}