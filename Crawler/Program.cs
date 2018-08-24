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
using System.Linq;

namespace Crawler
{
    class Program
    {
        public static CurlingTracker.ILogger Logger = new CurlingTracker.ConsoleLogger();
        private static FakeDBEventService _eventService;

        private static void InitializeDB()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CurlingContext>();
            optionsBuilder.UseSqlite(Config.Values.GetConnectionString("DefaultConnection"));

            var context = new CurlingContext(Config.Values.GetConnectionString("DefaultConnection"), optionsBuilder.Options);
            _eventService = new FakeDBEventService(context);
            
        }
        static void Main(string[] args)
        {
            try 
            {
                Logger.Log("Starting Program");
                InitializeDB();
                Logger.Log("Adding New Events");
                AddNewEvents();
                Logger.Log("Updating Events");
                UpdateEvents();
                Logger.Log("Ending Program");
            }
            catch(Exception ex)
            {
                Logger.Log(ex);
            }
        }

        public async static void AddNewEvents()
        {
            List<string> czIds = Request.GetCurrentCZIDs();
            List<string> newEventIds = await GetNewEventsFromEventList(czIds);
            Logger.Log("New Event IDs", newEventIds);
            foreach(string czId in newEventIds)
            {
                Event e = Request.GetEvent(czId);
                await _eventService.AddEventAsync(e);
            }
        }
        
        public async static void UpdateEvents()
        {
            Event[] events = await _eventService.GetUnfinishedEventsAsync();
            Logger.Log("Unfinished Events", events.ToList());
            foreach(Event e in events)
            {
              Event ev = Request.UpdateEvent(e);
              await _eventService.UpdateEventAsync(ev);
            }
        }

        async private static Task<List<string>> GetNewEventsFromEventList(List<string> czIds)
        {
            Event[] events = await _eventService.GetAllEventsAsync();
            Logger.Log("All Events", events.ToList());
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
