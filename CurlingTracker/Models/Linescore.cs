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

        public int Team1Score
        {
            get
            {
                return GetTeamXScore(1);      
            }
        }

        public int Team2Score
        {
            get
            {
                return GetTeamXScore(2);
            }
        }

        public bool Team1Hammer
        {
            get
            {
                return GetTeamXHammer(1);
            }
        }

        public bool Team2Hammer
        {
            get
            {
                return GetTeamXHammer(2);
            }
        }
        public int GetCurrentEnd()
        {
            return Ends.Count + 1;
        }

        private int GetTeamXScore(int x)
        {
            int score = 0;
            foreach(KeyValuePair<int, End> end in Ends)
            {
                if (x == 1)
                {
                    score += end.Value.Team1Score;
                }
                else
                {
                    score += end.Value.Team2Score;
                }
            }
            return score;
        }

        private bool GetTeamXHammer(int x, int UpperBoundOnEnds = 14)
        {
            bool team1Hammer = false;
            for (var endNumber = 1; endNumber <= UpperBoundOnEnds; endNumber++)
            {
                if (this.Ends.ContainsKey(endNumber))
                {
                    if (this.Ends[endNumber].Team1Hammer)
                    {
                        team1Hammer = true;
                    }
                    else
                    {
                        team1Hammer = false;
                    }
                }
            }
            if (x == 1)
            {
                return team1Hammer;
            }
            else 
            {
                return !team1Hammer;
            }
        }
    }
}