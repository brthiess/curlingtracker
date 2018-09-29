using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using CurlingTracker.Models;
using System.Linq;
using System.Threading;

namespace Crawler
{
    public static class Request
    {
        public static List<string> GetCurrentCZIDs()
        {
            Program.Logger.Log("Getting Current CZIDs");
            string currentEventPageHtml = GetHtml(GetCurrentEventPageUrl());
            List<string> eventIds = Parser.GetCurrentEventIds(currentEventPageHtml);
            return eventIds;
        }

        private static string GetCurrentEventPageUrl()
        {
            return Configuration.Values["endpoints:currentEvents"];
        }

        private static string GetSubEventUrl(string czGameId)
        {
            return Configuration.Values["endpoints:subEventInfo"].Replace("[CZ_GAME_ID]", czGameId);
        }


        private static string GetMainEventUrl(string czEventId)
        {
            return Configuration.Values["endpoints:drawsInfo"].Replace("[CZ_EVENT_ID]", czEventId);
        }

        private static string GetCZEventUrl(string czEventId)
        {
            return Configuration.Values["endpoints:czMainEventPage"].Replace("[CZ_EVENT_ID]", czEventId);
        }

        public static string GetPlayoffUrl(string czEventId)
        {
            return Configuration.Values["endpoints:czPlayoffPage"].Replace("[CZ_EVENT_ID]", czEventId);
        }

        public static string GetHtml(string url)
        {
            url = FormatLink(url);
            url = (url.LastIndexOf("#") > 0 ? url.Substring(0, url.LastIndexOf("#")) : url);
            int attemptNumber = 1;
            while (attemptNumber <= 3)
            {
                Program.Logger.Log("Attempt #" + attemptNumber + " to get URL: " + url);
                int crawlDelayInMilliseconds = int.Parse(Configuration.Values["misc:crawlDelay"]) * 1000;
                Thread.Sleep(crawlDelayInMilliseconds);
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        string htmlCode = client.DownloadString(url);
                        return htmlCode;
                    }
                }
                catch (Exception ex)
                {
                    if (attemptNumber == 3)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                attemptNumber++;
            }
            return "";
        }

        public static string FormatLink(string link)
        {
            string formattedLink = link;
            if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
            {
                Program.Logger.Log("Found relative link: " + link);
                formattedLink = Configuration.Values["endpoints:baseCZUrl"] + link;
                Program.Logger.Log("Formatted Link: " + formattedLink);
                if (!Uri.IsWellFormedUriString(formattedLink, UriKind.Absolute))
                {
                    throw new Exception("Invalid link: " + formattedLink);
                }
            }
            return formattedLink;
        }

        public static Event GetEvent(string czEventId)
        {
            string drawsJson = GetDrawsJsonFromCZId(czEventId);
            string czGameId = Parser.GetRandomGameIdFromMainEventJson(drawsJson);
            string subEventJson = GetHtml(GetSubEventUrl(czGameId));
            Event e = Parser.GetEventInfoFromJson(subEventJson);
            Program.Logger.Log("GetEvent", e);
            List<Draw> draws = Parser.GetEventDraws(drawsJson, e.EventId);
            e.Draws = draws;

            return e;
        }

        public static string GetCZEventPage(string czEventId)
        {
            return GetHtml(GetCZEventUrl(czEventId));
        }

        private static string GetDrawsJsonFromCZId(string czEventId)
        {
            string drawsJson = GetHtml(GetMainEventUrl(czEventId));
            return drawsJson;
        }
        public static string GetSubEventJsonFromCZId(string czEventId)
        {
            string drawsJson = GetHtml(GetMainEventUrl(czEventId));
            string czGameId = Parser.GetRandomGameIdFromMainEventJson(drawsJson);
            string subEventJson = GetHtml(GetSubEventUrl(czGameId));
            return subEventJson;
        }

        public static Event GetEventInfo(string czId)
        {
            string subEventJson = Request.GetSubEventJsonFromCZId(czId);
            Event e = Parser.GetEventInfoFromJson(subEventJson);
            return e;

        }

        public static Event UpdateEventInfo(Event e)
        {
            string subEventJson = Request.GetSubEventJsonFromCZId(e.CZId);
            e = Parser.GetEventInfoFromJson(subEventJson, e);
            return e;
        }

        public static Event UpdateEvent(Event e)
        {
            e = UpdateEventInfo(e);

            bool isOverAndFullyParsed = true;
            for (int i = 0; i < e.Draws.Count; i++)
            {
                if (!e.Draws[i].IsOverAndFullyParsed)
                {
                    isOverAndFullyParsed = false;
                    e.Draws[i] = UpdateDraw(e.Draws[i], e.CZId);
                }
            }
            e.Draws = RemoveEmptyAndDoneDraws(e.Draws);
            if (DateTime.Now.AddHours(-24) < e.EndDate)
            {
                isOverAndFullyParsed = false;
            }

            e.IsOverAndFullyParsed = isOverAndFullyParsed;
            Program.Logger.Log("Updated Event Info", e);
            return e;
        }

        private static List<Draw> RemoveEmptyAndDoneDraws(List<Draw> draws)
        {
            draws = draws.Where(d => !d.IsOverAndFullyParsed || d.Games.Count > 0).ToList();
            return draws;
        }

        private static Draw UpdateDraw(Draw d, string czId)
        {
            Program.Logger.Log("UpdateDraw", d);
            List<Game> games = new List<Game>();
            bool isOverAndFullyParsed = true;
            if (Parser.ShouldUpdateDraw(d.Date))
            {
                games = Parser.GetGamesByDrawDisplayNameAndDate(d.DisplayName, d.Date, GetHtml(GetMainEventUrl(czId)), d.EventId, d.DrawId);
                isOverAndFullyParsed = Parser.GamesAreAllOverAndFullyParsed(games);
            }
            else
            {
                Program.Logger.Log("Skipping Draw because it hasn't started yet.");
                Program.Logger.Log("DrawStart: " + d.Date.ToString());
                isOverAndFullyParsed = false;
            }


            if (DateTime.Now.AddHours(-12) < d.Date)
            {
                isOverAndFullyParsed = false;
            }
            d.IsOverAndFullyParsed = isOverAndFullyParsed;
            d.Games = games;
            return d;
        }

        public static string GetGameJson(string czGameId)
        {
            return GetHtml(GetSubEventUrl(czGameId));
        }
    }
}