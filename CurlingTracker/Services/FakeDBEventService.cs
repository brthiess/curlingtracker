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
                return await _context.Events
                .Include(e => e.Draws)
                    .ThenInclude(d => d.Games)
                        .ThenInclude(g => g.Linescore)
                 .Include(e => e.Draws)
                    .ThenInclude(d => d.Games)
                        .ThenInclude(g => g.Team1)
                            .ThenInclude(t => t.Players)
                .Include(e => e.Draws)
                    .ThenInclude(d => d.Games)
                        .ThenInclude(g => g.Team2)    
                             .ThenInclude(t => t.Players)    
                .Include(e => e.Type).ToArrayAsync();
        }
        public async Task<Event[]> GetCurrentEventsAsync()
        {
            Event[] events = await GetAllEventsAsync();
            return events.Where(e => e.EndDate < DateTime.Now).ToArray();
        }
        
         public async Task<Event[]> GetUnfinishedEventsAsync()
        {
            return await _context.Events.Where(x => x.IsOverAndFullyParsed == true).ToArrayAsync();
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