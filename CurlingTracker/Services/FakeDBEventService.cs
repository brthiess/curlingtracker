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

        public async Task<Event[]> GetAllEventsAsync()
        {          
            Event[] events = await _context.Events
                .Include(e => e.Type)
                .ToArrayAsync();    
            for(var i = 0; i < events.Length; i++)
            {
                events[i].Draws = await GetAllDrawsByEventId(events[i].EventId);
            }                              
                return events;
        }

        private async Task<List<Draw>> GetAllDrawsByEventId(Guid eventId)
        {
            Draw[] draws = await _context.Draws
                .Where(d => d.EventId.ToString() == eventId.ToString())
                .ToArrayAsync();
            for (var i = 0; i < draws.Length; i++)
            {
                draws[i] = await GetDrawAsync(draws[i].DrawId.ToString());
            }

            if (draws != null)
            {
                return draws.OrderBy(d => d.Date).ToList();                
            }
            return null;
        }

        
        public async Task<Event[]> GetCurrentEventsAsync()
        {
            Event[] events = await GetAllEventsAsync();
            return events.Where(e => e.EndDate < DateTime.Now).ToArray();
        }
        
         public async Task<Event[]> GetUnfinishedEventsAsync()
        {
            Event[] events = await GetAllEventsAsync();
            events = events.Where(e => e.IsOverAndFullyParsed == false).ToArray();
            return events;
        }

        public async Task<Event> GetEventAsync(string eventId)
        {
            Event[] events = await GetAllEventsAsync();
            events = events.Where(x => x.EventId.ToString() == eventId).ToArray();
            if (events.Count() > 0)
            {
                return events[0];
            }
            return null;
        }

        public async Task<Event> GetEventByCzIdAsync(string eventId)
        {
            Event[] events = await _context.Events.Where(x => x.CZId.ToString() == eventId).ToArrayAsync();
            if (events.Count() > 0)
            {
                return events[0];
            }
            return null;
        }
        public async Task<Game> GetGameAsync(string gameId)
        {
            Game[] games = await _context.Games
                .Include(g => g.Linescore)
                .Include(g => g.Team1)
                    .ThenInclude(t => t.Players)
                .Include(g => g.Team2)
                    .ThenInclude(t => t.Players)
                .Where(x => x.GameId.ToString() == gameId).ToArrayAsync();
            if (games.Count() > 0)
            {
                return games[0];
            }
            return null;  
        }

        public async Task<Draw> GetDrawAsync(string drawId)
        {
            Draw[] draws = await _context.Draws
                .Include(d => d.Games)
                    .ThenInclude(g => g.Team1)
                        .ThenInclude(t => t.Players)
                .Include(d => d.Games)
                    .ThenInclude(g => g.Team2)
                        .ThenInclude(t => t.Players)
                .Include(d => d.Games)
                    .ThenInclude(g => g.Linescore)
                .Where(x => x.DrawId.ToString() == drawId).ToArrayAsync();
            if (draws.Count() > 0)
            {
                return draws[0];
            }
            return null;  
        }

        public async Task<bool> AddEventAsync(Event e)
        {
            _context.Events.Add(e);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
        
        public async Task<bool> UpdateEventAsync(Event e)
        {
            _context.Events.Update(e);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }

}