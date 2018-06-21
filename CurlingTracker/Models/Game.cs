using System;

namespace CurlingTracker.Models
{
    public class Game
    {
        public Game(Team team1, Team team2, Linescore linescore, bool isFinal){
            this.Team1 = team1;
            this.Team2 = team2;
            this.Linescore = linescore;
            this.Team1ShortName = GetTeamShortNameFromTeam(team1);
            this.Team2ShortName = GetTeamShortNameFromTeam(team2);
            this.IsFinal = isFinal;
        }
        public Guid GameId {get; set;}

        public Team Team1 {get;set;}

        public Team Team2 {get;set;}

        
        public string Team1ShortName {get;}
        public string Team2ShortName {get;}

        public int Team1Score
        {
            get
            {
                return Linescore.Team1Score;
            }
        }

        public int Team2Score
        {
            get
            {
                return Linescore.Team2Score;
            }
        }

        public bool Team1Hammer 
        {
            get
            {
                return Linescore.Team1Hammer;
            }
        }

        public bool Team2Hammer 
        {
            get
            {
                return Linescore.Team2Hammer;
            }
        }

        public int CurrentEnd
        {
            get
            {
                return Linescore.GetCurrentEnd();
            }
        }

        public Linescore Linescore {get;set;}


        public bool IsFinal {get;set;}

        private string GetTeamShortNameFromTeam(Team team)
        {
            if (team.TeamType == EventType.TeamType.MIXED_DOUBLES)
            {
                return GetMixedDoublesTeamShortNameFromTeam(team);
            }
            else
            {
                return GetClassicTeamShortNameFromTeam(team);
            }
        }
        
        private string GetClassicTeamShortNameFromTeam(Team team)
        {
            string teamName = "";
            foreach(Player player in team.Players)
            {
                if (player.IsSkip)
                {
                    teamName += player.LastName;
                }
            }
            return teamName;
        }
        private string GetMixedDoublesTeamShortNameFromTeam(Team team)
        {
            string teamName = "";
            int i = 0;
            foreach(Player player in team.Players)
            {
                teamName += player.LastName;
                if (i == 0)
                {
                    teamName += "/";
                }
                i++;
            }
            return teamName;
        }
    }
}