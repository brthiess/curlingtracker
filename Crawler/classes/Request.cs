using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using CurlingTracker.Models;


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
                    //log exception
                }
                attemptNumber++;
            }
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

        public static string GetGameJson(string czGameId)
        {
            return GetHtml(GetSubEventUrl(czGameId));
        }
    }
}