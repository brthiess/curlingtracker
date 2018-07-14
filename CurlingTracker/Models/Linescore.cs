using System;
using System.Collections.Generic;

namespace CurlingTracker.Models
{
    public class Linescore
    {
        public Guid LinescoreId {get; set;}
        
        public int NumberOfEnds {get;set;}

        public Dictionary<int, End> Ends {get;set;}

        ///Number of ends in the game (not including extra end)
        public Linescore(int numberOfEnds)
        {
            this.Ends = new Dictionary<int, End>();
            this.NumberOfEnds = numberOfEnds + 1; //Add one for extra end
        }
        public Linescore(Dictionary<int, End> ends){
            this.Ends = ends;
        }
                
        public void AddEnd(End end)
        {
            if (Ends.Count == this.NumberOfEnds)
            {
                throw new Exception("Cannot add another end.  Already reached the maximum number of ends");
            }

            if (!Ends.ContainsKey(end.EndNumber))
            {
                Ends.Add(end.EndNumber, end);
            }
            else
            {
                throw new Exception("Linescore already contains this end number");
            }
        }

        public int GetTeamXScoreInEnd(int teamNumber, int endNumber)
        {
            int score = -1;
            if (teamNumber == 1)
            {
                score = Ends[endNumber].Team1Score;
            }
            else 
            {
                score = Ends[endNumber].Team2Score;
            }
            return score;
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

        private int  _currentEnd = -1;
        public int GetCurrentEnd()
        {
            if (_currentEnd != -1)
            {
                return _currentEnd;
            }
            
            int currentEnd = 1;
            foreach(KeyValuePair<int, End> end in Ends)
            {
                if(!end.Value.IsOver)
                {
                    break;
                }
                currentEnd++;
            }
            _currentEnd = currentEnd;
            return currentEnd;
        }

        public int GetNumberOfEnds()
        {
            return this.NumberOfEnds;
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