using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurlingTracker.Models;
using CurlingTracker.Utility;
using RandomNameGenerator;

namespace CurlingTracker.Services
{
    public class FakeEventService : IEventService
    {
        public Task<Event[]> GetCurrentEventsAsync()
        {
            
            return Task.FromResult(new[] { item1, item2 });
        }

        private List<Event> GetRandomEvents(int amount)
        {
            List<Event> Events = new List<Event>();
            for(int i = 0; i < amount; i++) 
            {
                Events.Add(GetRandomEvent());
            }
            return Events;
        }
        private Event GetRandomEvent()
        {
            return new Event(GetRandomEventName(), DateTime.Now.AddDays(-3), DateTime.Now.AddDays(2), GetRandomLocation(), GetRandomEventType(), GetDraws(7));
        }

        private string GetRandomEventName()
        {
            return "The " +  NameGenerator.GenerateLastName() + " Classic";
        }

        private string GetRandomLocation()
        {
            string[] Cities = new string[7] {"Edmonton, AB", "Winnipeg, MB", "Saskatoon, SK", "Toronto, ON", "St. John's, NF", "Stockholm, Sweden", "Hamburg, DE"};
            Random rand = new Random(DateTime.Now.Second);
            return Cities[rand.Next(0, Cities.Length - 1)];
        }
        //Gets random list of draws
        private List<Draw> GetDraws(int amount) 
        {
            List<Draw> draws = new List<Draw>();

            for(int i = 0; i < amount; i++)
            {
                draws.Add(GetRandomDraw());
            }
            return draws;
        }

        private Draw GetRandomDraw()
        {
            return new Draw(DateUtil.RandomDay());
        }
    }
}