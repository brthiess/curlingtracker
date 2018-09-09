using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurlingTracker.Models
{
    public class Bracket : IPrintable
    {
        [Required]
        public Guid BracketId {get;set;}
        
        [Required]
        public string Name {get;set;}

        [Required]
        public string Html {get;set;}

        public Bracket(string name, string html)
        {
            this.Name = name;
            this.Html = html;
        }

        public string Print()
        {
            return this.Name;
        }
    }
}