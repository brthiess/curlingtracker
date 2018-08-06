using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using CurlingTracker.Data;
using CurlingTracker.Services;
using CurlingTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {

        private static FakeDBEventService _eventService;

        private static void InitializeDB()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CurlingContext>();
            optionsBuilder.UseSqlite(Config.Values["connectionString"]);

            var context = new CurlingContext(optionsBuilder.Options);
            _eventService = new FakeDBEventService(context);
            
        }
        static void Main(string[] args)
        {
            InitializeDB();
            AddNewEvents();
            UpdateEvents();
        }

        public async static void AddNewEvents()
        {
            List<string> czIds = Request.GetCurrentCZIDs();
            List<string> newEventIds = await GetNewEventsFromEventList(czIds);
            foreach(string czId in newEventIds)
            {
                Event e = Request.GetEvent(czId);
                await _eventService.AddEventAsync(e);
            }
        }
        
        public async static void UpdateEvents()
        {
            Event[] events = await _eventService.GetUnfinishedEventsAsync();
            foreach(Event event in events)
            {
              Event e = Request.UpdateEvent(event);
              await _eventService.UpdateEventAsync(e);
            }
        }

        async private static Task<List<string>> GetNewEventsFromEventList(List<string> czIds)
        {
            Event[] events = await _eventService.GetAllEventsAsync();
            foreach (Event e in events)
            {
                if (czIds.Contains(e.CZId))
                {
                    czIds.Remove(e.CZId);
                }
            }
            return czIds;
        }
    }
}
