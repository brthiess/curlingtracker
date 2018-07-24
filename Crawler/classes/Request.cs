using System;
using System.IO;
using System.Net;
using System.Collections.Generic;



namespace Crawler
{
    public static class Request
    {
        private static string BaseUrl
        {
            get 
            {
                return Config.Values["baseUrl"];
            }
        }
        
        public static List<string> GetCurrentCZIDs()
        {
            string currentEventPageHtml = GetHtml(GetCurrentEventPageUrl());
            List<string> eventIds = Parser.GetCurrentEventIds(currentEventPageHtml);
            return eventIds;
        }

        private static string GetCurrentEventPageUrl()
        {
            return BaseUrl + Config.Values["endpoints:currentEvents"];
        }

        private static string GetEventPageUrl(string czId)
        {
            return BaseUrl + Config.Values["endpoints:eventInfo"].Replace("[CZ_ID]", czId);
        }

        private static string GetHtml(string url)
        {
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(url);
                return htmlCode;
            }            
        }

        private static Event GetEventObject(string czId)
        {
            string html = GetHtml(GetEventPageUrl(czId));
        }

    }
}