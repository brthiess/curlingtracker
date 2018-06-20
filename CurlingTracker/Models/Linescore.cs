using System;
using System.Collections.Generic;

namespace CurlingTracker.Models
{
    public class Linescore
    {
        public Linescore(List<End> ends){
            this.Ends = ends;
        }
        public Guid LinescoreId {get; set;}

        public List<End> Ends {get;set;}
                
    }
}