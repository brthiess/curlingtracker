using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CurlingTracker.Models
{
    public class Game
    {
        public Guid GameId {get; set;}

        [Required]
        public Team Team1 {get;set;}

        [Required]
        public Team Team2 {get;set;}

        [Required]
        public Guid EventId {get;set;}

        public bool PercentagesAvailable {get;set;}

        [Required]
        public Linescore Linescore {get;set;}

        [Required]
        public bool IsFinal {get;set;}

        public bool IsOverAndFullyParsed{get;set;}

        public Guid DrawId {get;set;}

        public Game(){}
        public Game(Team team1, Team team2, Linescore linescore, bool isFinal, Guid eventId, bool isOverAndFullyParsed, Guid drawId){
            this.Team1 = team1;
            this.Team2 = team2;
            this.Linescore = linescore;
            this.IsFinal = isFinal;
            this.PercentagesAvailable = false;
            this.EventId = eventId;
            this.DrawId = drawId;
            this.IsOverAndFullyParsed = isOverAndFullyParsed;
        }
        
        [NotMapped]
        public string Team1ShortName {
            get
            {
                return Team1.Name;
            }
        }

        [NotMapped]
        public string Team2ShortName {
            get
            {
                return Team2.Name;
            }
        }

        [NotMapped]
        public int Team1Score
        {
            get
            {
                return Linescore.Team1Score;
            }
        }

        [NotMapped]
        public int Team2Score
        {
            get
            {
                return Linescore.Team2Score;
            }
        }

        [NotMapped]
        public bool Team1Hammer 
        {
            get
            {
                return Linescore.Team1Hammer;
            }
        }

        [NotMapped]
        public bool Team2Hammer 
        {
            get
            {
                return Linescore.Team2Hammer;
            }
        }

        [NotMapped]
        public int CurrentEnd
        {
            get
            {
                return Linescore.GetCurrentEnd();
            }
        }


        public int GetScoreForEnd(int teamNumber, int endNumber)
        {
            return Linescore.GetTeamXScoreInEnd(teamNumber, endNumber);
        }

        public int GetNumberOfEnds()
        {
            return Linescore.GetNumberOfEnds();
        }
    }
}