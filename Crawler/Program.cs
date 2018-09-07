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
            Logger.Log("Connecting to DB with connection string: " + Config.Values.GetConnectionString("DefaultConnection"));
            var context = new CurlingContext(Config.Values.GetConnectionString("DefaultConnection"), optionsBuilder.Options);
            _eventService = new FakeDBEventService(context);
            Logger.Log(_eventService.ToString());
        }
        static void Main(string[] args)
        {
            try
            {
                Logger.Log("Starting Program");
                InitializeDB();
                Logger.Log("Adding New Events");
                AddNewEvents().Wait();
                Logger.Log("Updating Events");
                UpdateEvents().Wait();
                Logger.Log("Ending Program");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        public async static Task AddNewEvents()
        {
            List<string> czIds = Request.GetCurrentCZIDs();
            List<string> newEventIds = await GetNewEventsFromEventList(czIds);
            Logger.Log("New Event IDs", newEventIds);
            foreach (string czId in newEventIds)
            {
                try
                {
                    Logger.Log("Get event with CZId: " + czId);
                    Event e = Request.GetEvent(czId);
                    Logger.Log("AddNewEvents", e);
                    await _eventService.AddEventAsync(e);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }
            }
        }

        public async static Task UpdateEvents()
        {
            Logger.Log("UpdateEvents()");
            Event[] events = await _eventService.GetUnfinishedEventsAsync();
            Logger.Log("Unfinished Events", events.ToList());
            foreach (Event e in events)
            {
                try
                {
                    Logger.Log("Updating Event", e);
                    Event ev = Request.UpdateEvent(e);
                    await _eventService.UpdateEventAsync(ev);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }
            }
        }

        async private static Task<List<string>> GetNewEventsFromEventList(List<string> czIds)
        {
            Logger.Log("GetNewEventsFromEventList()");
            Event[] events = await _eventService.GetAllEventsAsync();
            Logger.Log("All Events in DB", events.ToList());
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
