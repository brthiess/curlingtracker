using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using CurlingTracker.Models;
using System.Linq;


namespace Crawler
{
    public static class Request
    {       
        public static List<string> GetCurrentCZIDs()
        {
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
                    //log exception
                }
                attemptNumber++;
            }
            return "";
        }

        public static Event GetEvent(string czEventId)
        {
            string drawsJson = GetHtml(GetMainEventUrl(czEventId));
            string czGameId = Parser.GetRandomGameIdFromMainEventJson(drawsJson);
            string subEventJson = GetHtml(GetSubEventUrl(czGameId));
            Event e = Parser.GetEventInfoFromJson(subEventJson);
            List<Draw> draws = Parser.GetEventDraws(drawsJson, e.EventId);
            e.Draws = draws;
            
            return e;
        }
        
        public static Event UpdateEvent(Event e)
        {
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
            return e;
        }

        private static List<Draw> RemoveEmptyAndDoneDraws(List<Draw> draws)
        {
            draws = draws.Where(d => !d.IsOverAndFullyParsed || d.Games.Count > 0).ToList();
            return draws;
        }
        
        private static Draw UpdateDraw(Draw d, string czId)
        {
            List<Game> games = Parser.GetGamesByDrawDisplayNameAndDate(d.DisplayName, d.Date, GetHtml(GetMainEventUrl(czId)), d.EventId, d.DrawId);
            bool isOverAndFullyParsed = true;
            foreach(Game g in games)
            {
                if (!g.IsOverAndFullyParsed)
                {
                    isOverAndFullyParsed = false;
                }
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