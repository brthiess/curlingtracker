using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CurlingTracker.Models;
using CurlingTracker.Services;

namespace CurlingTracker.Controllers
{
    public class ApiController : Controller
    {
        private readonly IEventService _eventService;
        public ApiController(IEventService eventService) 
        {
            _eventService = eventService;
        }
        public async Task<IActionResult> Scores(string eventId)
        {
            ViewData["Layout"] = "_Blank";
            var item = await _eventService.GetEventAsync(eventId);
            return View(item);
        }

        public async Task<IActionResult> Game(string gameId)
        {
            ViewData["Layout"] = "_Blank";
            var item = await _eventService.GetGameAsync(gameId);
            return View(item);
        }
    }
}
