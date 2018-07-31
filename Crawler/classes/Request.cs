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

        private static string GetEventInfoUrl(string czGameId)
        {
            return Config.Values["endpoints:eventInfo"].Replace("[CZ_GAME_ID]", czGameId);
        }

        private static string GetEventMainInfoUrl(string czEventId)
        {
            return Config.Values["endpoints:eventMainInfo"].Replace("CZ_EVENT_ID", czEventId);
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
            string eventJson = GetHtml(GetEventMainInfoUrl(czEventId));
            
            string json = GetHtml(GetEventInfoUrl(czEventId));
            Event e = Parser.GetEventFromJson(json);
            return e;
        }

    }
}