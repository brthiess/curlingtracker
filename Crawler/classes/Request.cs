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
            return Config.Values["endpoints:currentEvents"];
        }

        private static string GetSubEventUrl(string czGameId)
        {
            return Config.Values["endpoints:subEventInfo"].Replace("[CZ_GAME_ID]", czGameId);
        }
        

        private static string GetMainEventUrl(string czEventId)
        {
            return Config.Values["endpoints:drawsInfo"].Replace("[CZ_EVENT_ID]", czEventId);
        }

        public static string GetHtml(string url)
        {
            int attemptNumber = 1;
            while (attemptNumber <= 3)
            {
                Program.Logger.Log("Attempt #" + attemptNumber + " to get URL: " + url);
                int crawlDelayInMilliseconds = int.Parse(Config.Values["misc:crawlDelay"]) * 1000;
                Thread.Sleep(crawlDelayInMilliseconds);
                try
                {
                     using (WebClient client = new WebClient())
                    {
                        string htmlCode = client.DownloadString(url);
                        return htmlCode;
                    }            
                }
                catch(Exception ex)
                {
                    if (attemptNumber == 3)
                    {
                        throw new Exception (ex.Message);
                    }
                }
                attemptNumber++;
            }
            return "";
        }

        public static Event GetEvent(string czEventId)
        {
            string drawsJson =  GetDrawsJsonFromCZId(czEventId);
            string czGameId = Parser.GetRandomGameIdFromMainEventJson(drawsJson);
            string subEventJson = GetHtml(GetSubEventUrl(czGameId));
            Event e = Parser.GetEventInfoFromJson(subEventJson);
            Program.Logger.Log("GetEvent", e);
            List<Draw> draws = Parser.GetEventDraws(drawsJson, e.EventId);
            e.Draws = draws;
            
            return e;
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
            for(int i = 0; i < e.Draws.Count; i++) 
            {
                if(!e.Draws[i].IsOverAndFullyParsed)
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
            if(Parser.ShouldUpdateDraw(d.Date))
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