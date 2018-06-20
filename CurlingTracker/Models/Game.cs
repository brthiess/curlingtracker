using System;

namespace CurlingTracker.Models
{
    public class Game
    {
        public Game(Team team1, Team team2){
            Team t = new Team();
            t.Type = Team.TeamType.MIXED_CLASSIC;
        }
        public Guid GameId {get; set;}

        public Team Team1 {get;set;}

        public Team Team2 {get;set;}
                
    }
}