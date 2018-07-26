using System;
using System.Collections.Generic;
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

        public static Event GetEventFromJson(string json)
        {
            var api = new Api(json).Values;
            int numberOfEnds = 8;
            int.TryParse(api.numberOfEnds, out numberOfEnds);
            Guid eventId = Guid.NewGuid();
            Event e = new Event(
                api.@event.displayName, 
                DateTime.Parse(api.@event.startDate), 
                DateTime.Parse(api.@event.endDate),
                api.@event.location,
                new EventType(GetTeamTypeFromDivision(api.@event.division), numberOfEnds, eventId),
                czId: api.@event.eventId,
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

    }
}