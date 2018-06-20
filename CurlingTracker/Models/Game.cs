using System;

namespace CurlingTracker.Models
{
    public class Game
    {
        public Game(Player team1, Player team2, Linescore linescore){
            this.Team1 = team1;
            this.Team2 = team2;
            this.Linescore = linescore;
        }
        public Guid GameId {get; set;}

        public Player Team1 {get;set;}

        public Player Team2 {get;set;}

        public Linescore Linescore {get;set;}
                
    }
}