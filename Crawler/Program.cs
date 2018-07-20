using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;

namespace Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            AddNewEvents();
        }

        public static void AddNewEvents()
        {
            List<string> eventIds = Request.GetCurrentEventIDs();

        }

        private static List<string> GetNewEventsFromEventList(List<string> eventIds)
        {
            
        }
    }
}
