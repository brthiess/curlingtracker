using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CurlingTracker.Models;
using CurlingTracker.Utility;
using CurlingTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace CurlingTracker.Services
{
    public class FakeDBEventService : IEventService
    {
        private readonly CurlingContext _context;
        public FakeDBEventService(CurlingContext context)
        {
            _context = context;
        }

        public async Task<Event[]> GetCurrentEventsAsync()
        {
            return await _context.Events.Where(x => x.StartDate > DateTime.Now).ToArrayAsync();
        }

        public async Task<Event> GetEventAsync(string eventId)
        {
            Event[] events = await _context.Events.Where(x => x.EventId.ToString() == eventId).ToArrayAsync();
            if (events.Count() > 0)
            {
                return events[0];
            }
            return null;
        }

        public async Task<Game> GetGameAsync(string gameId)
        {
            Game[] games = await _context.Games.Where(x => x.GameId.ToString() == gameId).ToArrayAsync();
            if (games.Count() > 0)
            {
                return games[0];
            }
            return null;  
        }

        public async Task<Draw> GetDrawAsync(string drawId)
        {
            Draw[] draws = await _context.Draws.Where(x => x.DrawId.ToString() == drawId).ToArrayAsync();
            if (draws.Count() > 0)
            {
                return draws[0];
            }
            return null;  
        }
    }

}