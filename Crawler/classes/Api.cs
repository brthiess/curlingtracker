using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Crawler
{
    public class Api
    {
        public RootObject Values { get; private set; }
        public Api(string json)
        {
            Values = JsonConvert.DeserializeObject<RootObject>(json);
        }
        public class EventWeek
        {
        }

        public class ApiEvent
        {
            public string eventId { get; set; }
            public EventWeek eventWeek { get; set; }
            public string endDate { get; set; }
            public string startDate { get; set; }
            public string displayName { get; set; }
            public string division { get; set; }
            public string location { get; set; }
            public object featuredDraw { get; set; }
            public string bracketURL { get; set; }
            public string playoffURL { get; set; }
            public string status { get; set; }
            public bool useTeamName { get; set; }
        }

        public class Draw
        {
            public string displayName { get; set; }
            public string drawName { get; set; }
            public string startsAt { get; set; }
            public List<object> games { get; set; }
        }

        public class DrawPairName
        {
        }

        public class DrawRaw
        {
            public string drawid { get; set; }
            public string drawname { get; set; }
            public string drawtime { get; set; }
            public string active { get; set; }
            public string startdraw { get; set; }
            public string draw_pair { get; set; }
            public string draw_pair_date { get; set; }
            public DrawPairName draw_pair_name { get; set; }
            public string filled { get; set; }
        }

        public class RootObject
        {
            public string gameId { get; set; }
            public string awayTeamId { get; set; }
            public string homeTeamId { get; set; }
            public string awayTeamShortName { get; set; }
            public string homeTeamShortName { get; set; }
            public string awayTeamDisplayName { get; set; }
            public string homeTeamDisplayName { get; set; }
            public bool homeHammer { get; set; }
            public bool awayHammer { get; set; }
            public string homeTotal { get; set; }
            public string awayTotal { get; set; }
            public string gameStatus { get; set; }
            public string lastPlayedEnd { get; set; }
            public string currentEnd { get; set; }
            public string numberOfEnds { get; set; }
            public List<string> homeScores { get; set; }
            public List<string> awayScores { get; set; }
            public ApiEvent apiEvent { get; set; }
            public Draw draw { get; set; }
            public DrawRaw drawRaw { get; set; }
            public string statusText { get; set; }
            public string awayTeamUrl { get; set; }
            public string homeTeamUrl { get; set; }
        }
    }


}