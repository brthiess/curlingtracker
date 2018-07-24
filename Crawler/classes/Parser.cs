using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Crawler
{
    public static class Parser 
    {
        // Gets the event ids (external IDs)
        public static List<string> GetCurrentEventIds(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var document = htmlDoc.DocumentNode;

            IEnumerable<HtmlNode> links = document.QuerySelectorAll(Config.Values["selectors:homePageCurrentEventIds"]);
            var eventIds = new List<string>();
            foreach(HtmlNode node in links)
            {
                string eventId = GetEventIdFromLink(node.Attributes["href"].Value);
                if (!string.IsNullOrEmpty(eventId))
                {
                    eventIds.Add(eventId.Trim());
                }
            }            
            return eventIds;
        }

        public static string GetEventIdFromLink(string link)
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
    }
}