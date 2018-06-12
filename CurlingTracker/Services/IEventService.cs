using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurlingTracker.Models;

namespace CurlingTracker.Services
{
    public interface IEventService
    {
        Task<Event[]> GetCurrentEventsAsync();
    }
}