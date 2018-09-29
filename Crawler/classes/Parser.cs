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

            IEnumerable<HtmlNode> links = document.QuerySelectorAll(Configuration.Values["selectors:homePageCurrentEventIds"]);
            var eventIds = new List<string>();
            foreach (HtmlNode node in links)
            {
                string eventId = GetEventIdFromLink(node.Attributes["href"].Value);
                if (!string.IsNullOrEmpty(eventId))
                {
                    eventIds.Add(eventId.Trim());
                }
            }
            Program.Logger.Log("Event Ids found on CZ.com", eventIds);
            return eventIds;
        }

        public static Event GetEventInfoFromJson(string json, Event e = null)
        {
            var game = Api.GetGameObject(json);
            int numberOfEnds = 8;
            int.TryParse(game.numberOfEnds, out numberOfEnds);
            Guid eventId = Guid.NewGuid();
            EventFormat eventFormat = GetEventFormat(game.@event.eventId);
            Standings standings = null;
            List<Bracket> brackets = null;
            if (eventFormat == EventFormat.ROUND_ROBIN)
            {
                standings = GetEventStandings(game.@event.eventId);
            }
            else if (eventFormat == EventFormat.KNOCKOUT)
            {
                brackets = GetBrackets(game.@event.eventId);
            }
            Playoff playoff = GetPlayoff(game.@event.eventId);

            //TODO More robust parsing for dates in case of error.
            if (e == null)
            {
                e = new Event(
                   game.@event.displayName,
                   DateTime.Parse(game.@event.startDate),
                   DateTime.Parse(game.@event.endDate),
                   game.@event.location,
                   new EventType(GetTeamTypeFromDivision(game.@event.division), numberOfEnds, eventId, eventFormat),
                   czId: game.@event.eventId,
                   eventId: eventId,
                   standings: standings,
                   brackets: brackets,
                   playoff: playoff
               );
            }
            else
            {
                e.Name = game.@event.displayName;
                e.StartDate = DateTime.Parse(game.@event.startDate);
                e.EndDate = DateTime.Parse(game.@event.endDate);
                e.Location = game.@event.location;
                e.Type.SetTeamType(GetTeamTypeFromDivision(game.@event.division));
                e.Type.NumberOfEnds = numberOfEnds;
                e.Type.EventFormat = eventFormat;
                e.Standings = standings;
                e.Brackets = brackets;
                e.Playoff = playoff;
            }
            return e;
        }

        public static Playoff GetPlayoff(string czId)
        {
            string html = Request.GetHtml(Request.GetPlayoffUrl(czId));
            HtmlNode document = GetHtmlNode(html);
            IEnumerable<HtmlNode> bracketNodes = document.QuerySelectorAll(Configuration.Values["selectors:bracketsHtml"]);
            if (bracketNodes.Count() == 0)
            {
                return null;
            }
            string playoffHtml = bracketNodes.First().OuterHtml;
            string formattedPlayoffHtml = Formatter.Format.FormatPlayoff(playoffHtml);
            return new Playoff(formattedPlayoffHtml);
        }

        private static List<Bracket> GetBrackets(string czId)
        {
            List<Bracket> brackets = new List<Bracket>();
            List<string> bracketPageLinks = GetBracketsPageLinks(czId);
            foreach (string link in bracketPageLinks)
            {
                brackets.Add(GetBracketFromUrl(link));
            }
            return brackets;
        }

        public static Bracket GetBracketFromUrl(string url)
        {
            string html = Request.GetHtml(url);
            HtmlNode document = GetHtmlNode(html);
            IEnumerable<HtmlNode> bracketNodes = document.QuerySelectorAll(Configuration.Values["selectors:bracketsHtml"]);
            if (bracketNodes.Count() == 0)
            {
                return null;
            }
            string bracketsHtml = bracketNodes.First().OuterHtml;
            string formmattedStandingsHtml = Formatter.Format.FormatBracket(bracketsHtml);
            url = Request.FormatLink(url);
            Uri uri = new Uri(url);
            string bracketName = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("pdraw");
            return new Bracket(bracketName, formmattedStandingsHtml);
        }

        private static List<string> GetBracketsPageLinks(string czId)
        {
            string html = GetBracketsPageHtml(czId);
            HtmlNode document = GetHtmlNode(html);
            List<HtmlNode> bracketLinks = document.QuerySelectorAll(Configuration.Values["selectors:bracketPageBracketLinks"]).ToList();
            List<string> links = new List<string>();
            foreach (HtmlNode bracketLink in bracketLinks)
            {
                string link = bracketLink.GetAttributeValue("href", "");
                if (link != "")
                {
                    links.Add(link);
                }
            }
            return links;
        }

        private static string GetBracketsPageHtml(string czId)
        {
            return Request.GetHtml(Configuration.Values["endpoints:czBracketsPage"].Replace("[CZ_EVENT_ID]", czId));
        }

        private static Standings GetEventStandings(string czId)
        {
            string standingsHtml = GetStandingsHtml(czId);
            return new Standings(standingsHtml);
        }

        public static string GetStandingsHtml(string czId)
        {
            string html = Request.GetHtml(Configuration.Values["endpoints:czStandingsPage"].Replace("[CZ_EVENT_ID]", czId));
            HtmlNode document = GetHtmlNode(html);
            string standingsHtml = document.QuerySelectorAll(Configuration.Values["selectors:standingsHtml"]).First().OuterHtml;           
            string formmattedStandingsHtml = Formatter.Format.FormatStandings(standingsHtml);
            return formmattedStandingsHtml;
        }
        public static EventFormat GetEventFormat(string czId)
        {
            string czEventPageHtml = Request.GetCZEventPage(czId);
            HtmlNode document = GetHtmlNode(czEventPageHtml);

            IEnumerable<HtmlNode> bracketLinks = document.QuerySelectorAll(Configuration.Values["selectors:bracketLinks"]);
            IEnumerable<HtmlNode> standingsLink = document.QuerySelectorAll(Configuration.Values["selectors:roundRobinLink"]);
            if (bracketLinks.Count() == 0 && standingsLink.Count() == 0)
            {
                throw new Exception("Unable to find event format for CZID: " + czId);
            }
            else if (bracketLinks.Count() == 0 && standingsLink.Count() >= 1)
            {
                Program.Logger.Log("Found Round Robin for CZID: " + czId);
                return EventFormat.ROUND_ROBIN;
            }
            else
            {
                Program.Logger.Log("Found Knockout for CZID: " + czId);
                return EventFormat.KNOCKOUT;
            }
        }

        private static HtmlNode GetHtmlNode(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc.DocumentNode;
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
            else if (division == "mixed doubles")
            {
                return EventType.TeamType.MIXED_DOUBLES;
            }
            return EventType.TeamType.MEN;
        }

        public static string GetRandomGameIdFromMainEventJson(string json)
        {
            var draws = Api.GetDrawsObject(json);
            if (draws.Length > 0)
            {
                foreach (Api.DrawObject drawObject in draws)
                {
                    if (drawObject.games.Count > 0)
                    {
                        return drawObject.games[0].gameId;
                    }
                }
            }
            return null;
        }

        public static Game GetGame(string czGameId, Guid eventId, Guid drawId)
        {
            string gameJson = Request.GetGameJson(czGameId);
            Api.GameObject apiGame = Api.GetGameObject(gameJson);
            Team team1 = GetTeam(apiGame, true);
            Team team2 = GetTeam(apiGame, false);
            Linescore linescore = GetLinescore(apiGame);
            bool isFinal = GetIsFinal(apiGame.gameStatus);

            long drawStartTimeInUnixTimeStamp = long.Parse(apiGame.draw.startsAt);
            DateTime drawStart = DateTimeOffset.FromUnixTimeSeconds(drawStartTimeInUnixTimeStamp).UtcDateTime;


            bool isOverAndFullyParsed = false;
            if (DateTime.Now.AddHours(-6) > drawStart && isFinal)
            {
                isOverAndFullyParsed = true;
            }
            Game game = new Game(team1, team2, linescore, isFinal, eventId, isOverAndFullyParsed, drawId);
            Program.Logger.Log("Getting Game", game);
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
            for (var endNumber = 1; endNumber <= numberOfEnds + 1 && (endNumber - 1) < apiGame.homeScores.Count; endNumber++)
            {
                int team1Score = Utility.ParseIntWithDefault(apiGame.homeScores[endNumber - 1], 0);
                int team2Score = Utility.ParseIntWithDefault(apiGame.awayScores[endNumber - 1], 0);
                if (team1Score > 0 && team2Score > 0)
                {
                    Program.Logger.Log("Found end where both teams score.  Arbitrarily setting team 2's score to 0.");
                    team2Score = 0;
                }
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
        public static Team GetTeam(Api.GameObject apiGame, bool isHomeTeam)
        {
            EventType.TeamType teamType = GetTeamTypeFromDivision(apiGame.@event.division);

            string html = "";
            string teamDisplayName = "";
            if (isHomeTeam)
            {
                html = Request.GetHtml(apiGame.homeTeamUrl);
                teamDisplayName = apiGame.homeTeamDisplayName;
            }
            else
            {
                html = Request.GetHtml(apiGame.awayTeamUrl);
                teamDisplayName = apiGame.awayTeamDisplayName;
            }

            List<Player> players = GetPlayers(html, teamType);
            Team team = new Team(teamType, players, teamDisplayName);
            return team;
        }

        private static List<Player> GetPlayers(string html, EventType.TeamType teamType)
        {
            HtmlNode document = GetHtmlNode(html);
            List<HtmlNode> names = document.QuerySelectorAll(Configuration.Values["selectors:teamPagePlayerFullNames"]).ToList(); ;
            List<HtmlNode> images = document.QuerySelectorAll(Configuration.Values["selectors:teamPagePlayerImages"]).ToList();
            List<HtmlNode> positions = document.QuerySelectorAll(Configuration.Values["selectors:teamPagePlayerPositions"]).ToList();

            var players = new List<Player>();
            bool addedSkip = false;
            for (var i = 0; i < names.Count; i++)
            {
                Player p = GetPlayer(names[i].InnerHtml, (images.Count > i ? images[i].InnerHtml : null), (positions.Count > i ? positions[i].InnerHtml : null), 4 - i);
                if (p.IsSkip)
                {
                    addedSkip = true;
                }
                players.Add(p);
            }
            int numberOfPlayers = EventType.GetNumberOfPlayersFromTeamType(teamType);
            Queue<Player.Position> positionsLeft = GetEmptyPositions(players, teamType);
            while (players.Count < numberOfPlayers)
            {
                Player.Position position = 0;
                if (positionsLeft.Count > 0)
                {
                    position = positionsLeft.Dequeue();
                }
                Player p = new Player(null, null, Gender.Unknown, position, (!addedSkip ? true : false));
                addedSkip = true;
                Program.Logger.Log("Adding Player", p);
                players.Add(p);
            }
            return players;
        }

        private static Queue<Player.Position> GetEmptyPositions(List<Player> players, EventType.TeamType teamType)
        {
            Player.Position[] positions = { Player.Position.Fourth, Player.Position.Third, Player.Position.Second, Player.Position.Lead };
            var emptyPositions = new Queue<Player.Position>();
            foreach (Player.Position position in positions)
            {
                bool foundPosition = false;
                foreach (Player p in players)
                {
                    if (position == p.position)
                    {
                        foundPosition = true;
                        break;
                    }
                }
                if (!foundPosition)
                {
                    emptyPositions.Enqueue(position);
                }
            }
            return emptyPositions;
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
                return 0;
                //throw new Exception("Unable to find position in GetPositionFromHtml.  number = " + positionNumber);
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
                bool isOverAndFullyParsed = true;
                Guid newDrawId = Guid.NewGuid();
                if (ShouldUpdateDraw(drawStart))
                {
                    games = GetGames(draw.games, eventId, newDrawId);
                    isOverAndFullyParsed = GamesAreAllOverAndFullyParsed(games);
                }
                else
                {
                    Program.Logger.Log("Skipping GetGames in GetEventDraws because it hasn't started yet.");
                    Program.Logger.Log("Draw Start: " + drawStart.ToString());
                    isOverAndFullyParsed = false;
                }

                if (DateTime.Now.AddHours(-12) < drawStart)//If game is not older than 12 hours old then 
                {
                    isOverAndFullyParsed = false;
                }
                if (games.Count > 0 || !isOverAndFullyParsed)
                {
                    Draw d = new Draw
                    (
                        drawStart,
                        draw.displayName,
                        games,
                        eventId,
                        isOverAndFullyParsed,
                        newDrawId
                    );
                    Program.Logger.Log("GetEventDraws. Add Draw", d);
                    drawsList.Add(d);
                }
            }
            return drawsList;
        }

        public static List<Game> GetGamesByDrawDisplayNameAndDate(string displayName, DateTime drawDate, string json, Guid eventId, Guid drawId)
        {
            var draws = Api.GetDrawsObject(json);
            List<Game> games = new List<Game>();
            foreach (Api.DrawObject draw in draws)
            {
                long drawStartTimeInUnixTimeStamp = long.Parse(draw.startsAt);
                DateTime drawStart = DateTimeOffset.FromUnixTimeSeconds(drawStartTimeInUnixTimeStamp).UtcDateTime;

                if (draw.displayName == displayName && drawStart == drawDate)
                {
                    foreach (Api.Game apiGame in draw.games)
                    {
                        Game g = GetGame(apiGame.gameId, eventId, drawId);
                        games.Add(g);
                    }
                }
            }
            return games;
        }
        public static List<Game> GetGames(List<Api.Game> apiGames, Guid eventId, Guid newDrawId)
        {
            var games = new List<Game>();
            foreach (Api.Game apiGame in apiGames)
            {
                Game g = GetGame(apiGame.gameId, eventId, newDrawId);
                Program.Logger.Log("GetEventDraws. Adding Game", g);
                games.Add(g);
            }
            return games;
        }

        public static bool GamesAreAllOverAndFullyParsed(List<Game> games)
        {
            bool isOverAndFullyParsed = true;
            foreach (Game g in games)
            {
                if (!g.IsOverAndFullyParsed)
                {
                    isOverAndFullyParsed = false;
                }
            }

            return isOverAndFullyParsed;
        }
        public static bool ShouldUpdateDraw(DateTime drawStart)
        {
            if (DateTime.Now.ToUniversalTime() > drawStart.AddHours(-2))
            {
                return true;
            }
            return false;
        }
    }
}