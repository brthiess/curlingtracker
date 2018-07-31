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
            return Config.Values["endpoints:mainEventInfo"].Replace("[CZ_EVENT_ID]", czEventId);
        }

        private static string GetHtml(string url)
        {
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(url);
                return htmlCode;
            }            
        }

        public static Event GetEvent(string czEventId)
        {
            string eventJson = GetHtml(GetMainEventUrl(czEventId));
            string czGameId = Parser.GetRandomGameIdFromMainEventJson(eventJson);
            string json = GetHtml(GetSubEventUrl(czGameId));
            Event e = Parser.GetEventFromJson(json);
            return e;
        }

    }
}