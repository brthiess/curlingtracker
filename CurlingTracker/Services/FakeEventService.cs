using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurlingTracker.Models;
using CurlingTracker.Utility;

namespace CurlingTracker.Services
{
    public class FakeEventService : IEventService
    {
        public Task<bool> AddEventAsync(Event e)
        {
            return Task.FromResult(true);
        }
        
        public Task<bool> UpdateEventAsync(Event e)
        {
            return Task.FromResult(true);
        }

        public Task<Event[]> GetAllEventsAsync()
        {
            Event[] events = GetRandomEvents(8).ToArray();
            return Task.FromResult(events);
        }
        
          public Task<Event[]> GetUnfinishedEventsAsync()
        {
            Event[] events = GetRandomEvents(8).ToArray();
            return Task.FromResult(events);
        }
        
        public Task<Event[]> GetCurrentEventsAsync()
        {
            Event[] events = GetRandomEvents(8).ToArray();
            return Task.FromResult(events);
        }

        public Task<Event> GetEventAsync(string eventId)
        {
            Event eventObject = GetRandomEvent();
            return Task.FromResult(eventObject);
        }

        public Task<Event> GetEventByCzIdAsync(string eventId)
        {
            Event eventObject = GetRandomEvent();
            return Task.FromResult(eventObject);
        }
        public Task<Game> GetGameAsync(string gameId)
        {
            EventType et = GetRandomEventType();
            Game gameObject = GetRandomGame(et);
            return Task.FromResult(gameObject);    
        }

        public Task<Draw> GetDrawAsync(string drawId)
        {
            EventType et = GetRandomEventType();
            Draw draw = GetRandomDraw(1, et);
            return Task.FromResult(draw);
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
            string location = GetRandomLocation();
            return new Event(GetRandomEventName(location), DateTime.Now.AddDays(-3), DateTime.Now.AddDays(2), location, eventType, GetDraws(7, eventType));
        }

        private EventType GetRandomEventType()
        {
            Random random = new Random();
            int numberOfEnds = (random.Next(0,2) == 1 ? 8 : 10);
            Array values = Enum.GetValues(typeof (EventType.TeamType));
            EventType randomEventType = new EventType((EventType.TeamType)values.GetValue(random.Next(values.Length)), numberOfEnds, Guid.NewGuid());
            return randomEventType;
        }

        private string GetRandomEventName(string location)
        {
            var random = new Random();
            string[] endingNames = {"Classic", "Spiel", "Cashspiel"};
            return "The " + location + " " +  StringUtil.FirstLetterToUpper(RandomNameGenerator.NameGenerator.GenerateLastName()) + " " + endingNames[random.Next(endingNames.Length)];
        }

        private string GetRandomLocation()
        {
            string[] Cities = new string[7] {"Edmonton, AB", "Winnipeg, MB", "Saskatoon, SK", "Toronto, ON", "St. John's, NF", "Stockholm, Sweden", "Hamburg, DE"};
            Random rand = new Random();
            return Cities[rand.Next(0, Cities.Length - 1)];
        }
        //Gets random list of draws
        private List<Draw> GetDraws(int amount, EventType eventType) 
        {
            List<Draw> draws = new List<Draw>();

            for(int i = 0; i < amount; i++)
            {
                draws.Add(GetRandomDraw(i, eventType));
            }
            return draws;
        }

        private Draw GetRandomDraw(int drawNumber, EventType eventType)
        {
            return new Draw(DateUtil.RandomDay(), "Draw " + (drawNumber + 1), GetGames(10, eventType), Guid.NewGuid());
        }

        private List<Game> GetGames(int amount, EventType eventType)
        {
            var games = new List<Game>();
            for(var i = 0; i < amount; i++)
            {
                games.Add(GetRandomGame(eventType));
            }
            return games;
        }

        private Game GetRandomGame( EventType eventType)
        {
            Team team1 = GetRandomTeam(eventType);
            Team team2 = GetRandomTeam(eventType);
            var random = new Random();
            int numberOfPlayedEnds = random.Next(eventType.NumberOfEnds + 1);
            bool isFinal = GetTrueWithProbability((double)numberOfPlayedEnds / (double)eventType.NumberOfEnds);
            var game = new Game(team1, team2, GetRandomLineScore(eventType.NumberOfEnds, numberOfPlayedEnds), isFinal, Guid.NewGuid(), isFinal);
            return game;
        }

        private bool GetTrueWithProbability(double probability)
        {
            var random = new Random();
            if (random.NextDouble() < probability)
            {
                return true;
            }
           return false;
        }

        private Linescore GetRandomLineScore(int numberOfEnds, int numberOfPlayedEnds)
        {
            var linescore = new Linescore(numberOfEnds);
            var rand = new Random();
            int team1Score = 0;
            int team2Score = 0;
            bool team1Hammer = (rand.Next() % 2 == 0);
            for(var endNumber = 1; endNumber <= numberOfPlayedEnds; endNumber++)
            {
                if (team1Score > 0)
                {
                    team1Hammer = false;
                }    
                else if (team2Score > 0)
                {
                    team1Hammer = true;
                }
                
                team1Score = 0;
                team2Score = 0;
                if (rand.Next() % 2 == 0)
                {
                    team1Score = GetRandomScore();
                }
                else 
                {
                    team2Score = GetRandomScore();
                }
                linescore.AddEnd(new End(team1Score, team2Score, team1Hammer, endNumber, true));
            }
            return linescore;
        }

        private int GetRandomScore()
        {
            var rand = new Random();
            var score = 0;
            int distribution = rand.Next(0,1000);
            if (distribution < 300)
            {
                score = 0;
            }
            else if (distribution < 600)
            {
                score = 1;
            }
            else if (distribution < 800) 
            {
                score = 2;
            }
            else if (distribution < 950)
            {
                score = 3;
            }
            else if (distribution < 980)
            {
                score = 4;
            }
            else if (distribution < 990)
            {
                score = 5;
            }
            else if (distribution < 999) 
            {
                score = 6;
            }
            else if (distribution < 1000) 
            {
                score = 8;
            }
            return score;
        }
        private Team GetRandomTeam(EventType eventType)
        {
            List<Player> players = GetRandomPlayers(eventType);
            return new Team(eventType.teamType, players);
        }

        private List<Player> GetRandomPlayers(EventType eventType)
        {
            var players = new List<Player>();
            int numberOfPlayers = eventType.NumberOfPlayers;
            Gender currentGender = eventType.Gender;
            
            for (var i = 0; i < numberOfPlayers; i++)
            {   
                if (eventType.Gender == Gender.Mixed && i < (numberOfPlayers / 2))
                {
                    currentGender = Gender.Male;
                }
                else if (eventType.Gender == Gender.Mixed && i >= (numberOfPlayers / 2))
                {
                    currentGender = Gender.Female;
                }
                players.Add(GetRandomPlayer(eventType.Gender, GetPositionFromNumber(i + 1), GetIsSkipFromPositionNumber(i + 1)));
            }
            return players;
        }

        private bool GetIsSkipFromPositionNumber(int position)
        {
            return (position == 4);
        }
        

        private Player.Position GetPositionFromNumber(int position)
        {
            if (position == 1)
            {
                return Player.Position.Lead;
            }
            else if (position == 2)
            {
                return Player.Position.Second;
            }
            else if (position == 3)
            {
                return Player.Position.Third;
            }
            else 
            {
                return Player.Position.Fourth;
            }
        }
        private Player GetRandomPlayer(Gender gender, Player.Position position, bool isSkip)
        {
            RandomNameGenerator.Gender g = RandomNameGenerator.Gender.Female;
            if (gender == Gender.Male)
            {
                g = RandomNameGenerator.Gender.Male;
            }
            Player p = new Player(StringUtil.FirstLetterToUpper(GetRandomName(true, g)), StringUtil.FirstLetterToUpper(GetRandomName(false, g)), gender, position, isSkip);
            p.SetRandomImage();
            return p;
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

        private string GetRandomName(bool firstName, RandomNameGenerator.Gender g)
        {
            string name = "";
            if (firstName)
            {
                name = RandomNameGenerator.NameGenerator.GenerateFirstName(g);
            }
            else 
            {
                var rand = new Random();
                name = RandomNameGenerator.NameGenerator.GenerateLastName() + (rand.Next() % 2 == 0 ? "-" + RandomNameGenerator.NameGenerator.GenerateLastName() : "");
            }
            return name;               
        }
    }
}