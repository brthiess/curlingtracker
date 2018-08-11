using System;
using System.Collections.Generic;

namespace CurlingTracker.Models
{
    public class End
    {
        public Guid EndId {get; set;}

        public int EndNumber {get;set;}
        
        public bool Team1Hammer {get;set;}
        
        public bool Team2Hammer {get;set;}

        public int Team1Score {get;set;}

        public int Team2Score {get;set;}

        public bool IsOver {get;set;}

        public End(int team1Score, int team2Score, bool team1Hammer, int endNumber, bool isOver){
            if (team1Score != 0 && team2Score != 0)
            {
                throw new Exception("Error creating 'End' object.  'team1Score' and 'team2Score' are both not 0.");
            }
            this.Team1Score = team1Score;
            this.Team2Score = team2Score;
            this.Team1Hammer = team1Hammer;
            this.Team2Hammer = !team1Hammer;
            this.EndNumber = endNumber;
            this.IsOver = isOver;
            this.EndId = Guid.NewGuid();
        }                
    }
}