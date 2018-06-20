using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurlingTracker.Models;
using CurlingTracker.Utility;

namespace CurlingTracker.Services
{
    public class FakeEventService : IEventService
    {
        public Task<Event[]> GetCurrentEventsAsync()
        {
            Event[] events = GetRandomEvents(5).ToArray();
            return Task.FromResult(events);
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
            EventType eventType = GetRandomEventType();
            return new Event(GetRandomEventName(), DateTime.Now.AddDays(-3), DateTime.Now.AddDays(2), GetRandomLocation(), eventType, GetDraws(7, GetRandomGender(), eventType));
        }

        private EventType GetRandomEventType()
        {
            Random random = new Random();
            Array values = Enum.GetValues(typeof (EventType.TeamType));
            EventType randomEventType = new EventType((EventType.TeamType)values.GetValue(random.Next(values.Length)));
            return randomEventType;
        }

        private string GetRandomEventName()
        {
            return "The " +  RandomNameGenerator.NameGenerator.GenerateLastName() + " Classic";
        }

        private string GetRandomLocation()
        {
            string[] Cities = new string[7] {"Edmonton, AB", "Winnipeg, MB", "Saskatoon, SK", "Toronto, ON", "St. John's, NF", "Stockholm, Sweden", "Hamburg, DE"};
            Random rand = new Random(DateTime.Now.Second);
            return Cities[rand.Next(0, Cities.Length - 1)];
        }
        //Gets random list of draws
        private List<Draw> GetDraws(int amount, Gender gender, EventType eventType) 
        {
            List<Draw> draws = new List<Draw>();

            for(int i = 0; i < amount; i++)
            {
                draws.Add(GetRandomDraw(i, gender, eventType));
            }
            return draws;
        }

        private Draw GetRandomDraw(int drawNumber, Gender gender, EventType eventType)
        {
            return new Draw(DateUtil.RandomDay(), "Draw " + drawNumber, GetGames(10, gender, eventType));
        }

        private List<Game> GetGames(int amount, Gender gender, EventType eventType)
        {
            var games = new List<Game>();
            for(var i = 0; i < amount; i++)
            {
                games.Add(GetRandomGame(gender, eventType));
            }
        }

        private Game GetRandomGame(Gender gender, EventType eventType)
        {
            Team team1 = GetRandomTeam(gender, eventType);
        }

        private Team GetRandomTeam(Gender gender, EventType eventType)
        {
            List<Player> players = GetRandomPlayers(gender, eventType);
            return new Team(eventType.teamType, players);
        }

        private List<Player> GetRandomPlayers(Gender gender, EventType eventType)
        {
            var players = new List<Player>();
            int numberOfPlayers = eventType.NumberOfPlayers;
            for (var i = 0; i < numberOfPlayers; i++)
            {   
                
            }
        }

        private Player GetRandomPlayer(Gender gender)
        {
            
        }

        private Gender GetRandomGender()
        {
            Random rand = new Random();
            if (rand.Next(0,2) == 1)
            {
                return Gender.Male;
            }
            else 
            {
                return Gender.Female;
            }
        }
    }
}