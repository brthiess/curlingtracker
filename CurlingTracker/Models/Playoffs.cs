using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurlingTracker.Models
{
    public class Playoff
    {
        [Required]
        public Guid PlayoffId {get;set;}

        [Required]
        public string Html {get;set;}

        public Playoff(string html)
        {
            this.Html = html;
            this.PlayoffId = Guid.NewGuid();
        }
    }
}