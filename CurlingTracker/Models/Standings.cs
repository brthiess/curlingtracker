using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurlingTracker.Models
{
    public class Standings : IPrintable
    {
        [Required]
        public Guid StandingsId {get;set;}


        [Required]
        public string Html {get;set;}

        public Standings(string html)
        {
            this.Html = html;
        }

        public string Print()
        {
            return "Standings: " + this.Html.Substring(0, 20) + "...";
        }
    }
}