using System;
using System.Collections.Generic;

namespace CurlingTracker.Models
{
    public class Linescore
    {
        public Linescore()
        {
            this.Ends = new Dictionary<int, End>();
        }
        public Linescore(Dictionary<int, End> ends){
            this.Ends = ends;
        }

        public Guid LinescoreId {get; set;}

        public Dictionary<int, End> Ends {get;set;}
                
        public void AddEnd(End end)
        {
            if (!Ends.ContainsKey(end.EndNumber))
            {
                Ends.Add(end.EndNumber, end);
            }
            else
            {
                throw new Exception("Linescore already contains this end number");
            }
        }
    }
}