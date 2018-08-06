using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Text.RegularExpressions;
using CurlingTracker.Models;
using Newtonsoft.Json;

namespace Crawler
{
    public static class Parser
    {
        // Gets the event ids (external IDs)
        public static List<string> GetCurrentEventIds(string html)
        {
            HtmlNode document = GetHtmlNode(html);

            IEnumerable<HtmlNode> links = document.QuerySelectorAll(Config.Values["selectors:homePageCurrentEventIds"]);
            var eventIds = new List<string>();
            foreach (HtmlNode node in links)
            {
                string eventId = GetEventIdFromLink(node.Attributes["href"].Value);
                if (!string.IsNullOrEmpty(eventId))
                {
                    eventIds.Add(eventId.Trim());
                }
            }
            return eventIds;
        }

        private static HtmlNode GetHtmlNode(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc.DocumentNode;
        }

        public static Event GetEventInfoFromJson(string json)
        {
            var game = Api.GetGameObject(json);
            int numberOfEnds = 8;
            int.TryParse(game.numberOfEnds, out numberOfEnds);
            Guid eventId = Guid.NewGuid();
            //TODO More robust parsing for dates in case of error.
            Event e = new Event(
                game.@event.displayName,
                DateTime.Parse(game.@event.startDate),
                DateTime.Parse(game.@event.endDate),
                game.@event.location,
                new EventType(GetTeamTypeFromDivision(game.@event.division), numberOfEnds, eventId),
                czId: game.@event.eventId,
                eventId: eventId
            );
            return e;
        }


        private static string GetEventIdFromLink(string link)
        {
            string eventId = "";
            Regex r = new Regex("eventid=([A-Za-z0-9]*)");
            if (r.IsMatch(link))
            {
                Match m = r.Match(link);
                eventId = m.Groups[1].Value;
            }
            return eventId;
        }
        private static EventType.TeamType GetTeamTypeFromDivision(string division)
        {
            if (division == "men")
            {
                return EventType.TeamType.MEN;
            }
            else if (division == "women")
            {
                return EventType.TeamType.WOMEN;
            }
            return EventType.TeamType.MEN;
        }

        public static string GetRandomGameIdFromMainEventJson(string json)
        {
            var draws = Api.GetDrawsObject(json);
            if (draws.Length > 0)
            {
                foreach(Api.DrawObject drawObject in draws)
                {
                    if (drawObject.games.Count > 0)
                    {
                        return drawObject.games[0].gameId;
                    }
                }
            }
            return null;
        }

        private static Game GetGame(string czGameId, Guid eventId)
        {
            string gameJson = Request.GetGameJson(czGameId);
            Api.GameObject apiGame = Api.GetGameObject(gameJson);
            Team team1 = GetTeam(apiGame, true);
            Team team2 = GetTeam(apiGame, false);
            Linescore linescore = GetLinescore(apiGame);
            Game game = new Game(team1, team2, linescore, GetIsFinal(apiGame.gameStatus), eventId);
            return game;
        }

        private static bool GetIsFinal(string gameStatus)
        {
            if (gameStatus == "final")
            {
                return true;
            }
            return false;
        }
        private static Linescore GetLinescore(Api.GameObject apiGame)
        {
            int numberOfEnds = 10;
            int.TryParse(apiGame.numberOfEnds, out numberOfEnds);

            int lastPlayedEnd = 10;
            int.TryParse(apiGame.lastPlayedEnd, out lastPlayedEnd);

            Linescore linescore = new Linescore(numberOfEnds);
            bool team1Hammer = apiGame.homeHammer;
            for(var endNumber = 1; endNumber <= numberOfEnds + 1 && (endNumber - 1) < apiGame.homeScores.Count; endNumber++)
            {
                int team1Score = Utility.ParseIntWithDefault(apiGame.homeScores[endNumber - 1], 0);
                int team2Score =Utility.ParseIntWithDefault(apiGame.awayScores[endNumber - 1], 0);

                var end = new End(
                    team1Score, 
                    team2Score,
                    team1Hammer,
                    endNumber,
                    (endNumber <= lastPlayedEnd)
                );
                linescore.AddEnd(end);

                if (team1Score > 0)
                {
                    team1Hammer = false;
                }
                else if (team2Score > 0) 
                {
                    team1Hammer = true;
                }
            }
            return linescore;
        }
        private static Team GetTeam(Api.GameObject apiGame, bool isHomeTeam)
        {
            EventType.TeamType teamType = GetTeamTypeFromDivision(apiGame.@event.division);
            string html = "";
            if (isHomeTeam)
            {
                html = Request.GetHtml(apiGame.homeTeamUrl);
            }
            else
            {
                html = Request.GetHtml(apiGame.awayTeamUrl);
            }

            List<Player> players = GetPlayers(html);
            Team team = new Team(teamType, players);
            return team;
        }

        private static List<Player> GetPlayers(string html)
        {
            HtmlNode document = GetHtmlNode(html);
            List<HtmlNode> names = document.QuerySelectorAll(Config.Values["selectors:teamPagePlayerFullNames"]).ToList(); ;
            List<HtmlNode> images = document.QuerySelectorAll(Config.Values["selectors:teamPagePlayerImages"]).ToList();
            List<HtmlNode> positions = document.QuerySelectorAll(Config.Values["selectors:teamPagePlayerPositions"]).ToList();

            var players = new List<Player>();
            for (var i = 0; i < names.Count; i++)
            {
                Player p = GetPlayer(names[i].InnerHtml, (images.Count > i ? images[i].InnerHtml : null), (positions.Count > i ? positions[i].InnerHtml : null), 4 - i);
                players.Add(p);
            }
            return players;
        }
        private static Player GetPlayer(string nameHtml, string imageHtml, string positionHtml, int positionNumber)
        {
            return new Player(
                GetFirstNameFromNameHtml(nameHtml), 
                GetLastNameFromNameHtml(nameHtml), 
                GetGenderFromName(nameHtml), 
                GetPositionFromHtml(positionHtml, positionNumber), 
                GetIsSkipFromHtml(positionHtml)
            );
        }

        private static string GetFirstNameFromNameHtml(string nameHtml)
        {
            return nameHtml.Split("<br>")[0];
        }

        private static string GetLastNameFromNameHtml(string nameHtml)
        {
            string[] nameSplit = nameHtml.Split("<br>");
            if (nameSplit.Length > 1)
            {
                return nameSplit[1];
            }
            return null;
        }

        private static Gender GetGenderFromName(string nameHtml)
        {
            return Gender.Unknown;
        }

        private static Player.Position GetPositionFromHtml(string positionHtml, int positionNumber)
        {
            if (positionHtml.ToLower() == "lead")
            {
                return Player.Position.Lead;
            }
            else if (positionHtml.ToLower() == "second")
            {
                return Player.Position.Second;
            }
            else if (positionHtml.ToLower() == "third")
            {
                return Player.Position.Third;
            }
            else if (positionHtml.ToLower() == "fourth")
            {
                return Player.Position.Fourth;
            }

            if (positionNumber == 1)
            {
                return Player.Position.Lead;
            }
            else if (positionNumber == 2)
            {
                return Player.Position.Second;
            }
            else if (positionNumber == 3)
            {
                return Player.Position.Third;
            }
            else if (positionNumber == 4)
            {
                return Player.Position.Fourth;
            }
            else 
            {
                throw new Exception("Unable to find position in GetPositionFromHtml.  number = " + positionNumber);
            }
        }

        private static bool GetIsSkipFromHtml(string positionHtml)
        {
            if (positionHtml.ToLower() == "skip")
            {
                return true;
            }
            return false;
        }

        public static List<Draw> GetEventDraws(string json, Guid eventId)
        {
            var draws = Api.GetDrawsObject(json);
            var drawsList = new List<Draw>();
            foreach (Api.DrawObject draw in draws)
            {
                long drawStartTimeInUnixTimeStamp = long.Parse(draw.startsAt);
                DateTime drawStart = DateTimeOffset.FromUnixTimeSeconds(drawStartTimeInUnixTimeStamp).UtcDateTime;
                List<Game> games = new List<Game>();
                foreach (Api.Game apiGame in draw.games)
                {
                    Game g = GetGame(apiGame.gameId, eventId);
                    games.Add(g);
                }
                Draw d = new Draw
                (
                    drawStart,
                    draw.displayName,
                    games,
                    eventId
                );
                drawsList.Add(d);
            }
            return drawsList;
        }
    }
}