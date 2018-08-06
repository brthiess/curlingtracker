using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurlingTracker.Models;

namespace CurlingTracker.Services
{
    public interface IEventService
    {
        Task<Event[]> GetAllEventsAsync();

        Task<Event[]> GetCurrentEventsAsync();
        
        Task<Event[]> GetUnfinishedEventsAsync();

        Task<Event> GetEventAsync(string eventId);

        Task<Event> GetEventByCzIdAsync(string czId);        

        Task<Game> GetGameAsync(string gameId);

        Task<Draw> GetDrawAsync(string drawId);

        Task<bool> AddEventAsync(Event e);
        
        Task<bool> UpdateEventAsync(Event e);
    }
}