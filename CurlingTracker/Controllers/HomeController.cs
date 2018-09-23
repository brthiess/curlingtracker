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
    public class IndexController : Controller
    {
        private readonly IEventService _eventService;
        public IndexController(IEventService eventService) 
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var currentEvents = await _eventService.GetCurrentEventsAsync();
            return View(currentEvents);
        }


        public async Task<IActionResult> Scores()
        {
            var items = await _eventService.GetCurrentEventsAsync();
            ViewData["DefaultEvent"] = null;
            if (items.Count() > 0)
            {
                ViewData["DefaultEvent"] = items[0];
                ViewData["DrawId"] = items[0].CurrentDraw.DrawId.ToString();
            }
            
            return View(items);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
