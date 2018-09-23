using System;
using System.Threading.Tasks;
using CurlingTracker.Data;
using CurlingTracker.Models;
using CurlingTracker.Services;
using Crawler;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crawler.UnitTests
{
    public class CrawlerTests
    {

        [Theory]
        [TestMethod]
        [TestCategory("Event")]
        [InlineData("5295", Gender.Male, "2018-08-30", "2018-09-02", "Oakville Fall Classic", EventType.TeamType.MEN, EventFormat.ROUND_ROBIN, false)]
        [InlineData("5422", Gender.Female, "2018-08-31", "2018-09-02", "Carleton Heights Labour Day U21 Spiel", EventType.TeamType.WOMEN, EventFormat.ROUND_ROBIN, false)]
        [InlineData("5304", Gender.Mixed, "2018-08-26", "2018-08-30", "Audi quattro Winter Games NZ Mixed Doubles", EventType.TeamType.MIXED_DOUBLES, EventFormat.ROUND_ROBIN, false)]
        [InlineData("5327", Gender.Male, "2018-09-20", "2018-09-23", "KW Fall Classic", EventType.TeamType.MEN, EventFormat.KNOCKOUT, true)]
        public void GetEventInfoTest(string czId, Gender g, string startDate, string endDate, string name, EventType.TeamType teamType, EventFormat eventFormat, bool standingsIsNull)
        {
            Console.WriteLine("GetEventInfoTest\nCZID: " + czId);
            Event e = Request.GetEventInfo(czId);
            Xunit.Assert.Equal(czId, e.CZId);
            Xunit.Assert.Equal(g, e.Type.Gender);
            Xunit.Assert.Equal(DateTime.Parse(startDate), e.StartDate);
            Xunit.Assert.Equal(DateTime.Parse(endDate), e.EndDate);
            Xunit.Assert.Equal(teamType, e.Type.teamType);
            Xunit.Assert.Equal(name, e.Name);
            Xunit.Assert.Equal(eventFormat, e.Type.EventFormat);
            Xunit.Assert.Equal(standingsIsNull, (e.Standings == null));
            Xunit.Assert.True(Utility.ValidatePlayoffHtml(e.Playoff.Html));
        }

        [Theory]
        [TestMethod]
        [TestCategory("FormatLink")]
        [InlineData("event.php?eventid=5327&view=Round-Robin&view=Round-Robin&pdraw=A#1", "http://curlingzone.com/event.php?eventid=5327&view=Round-Robin&view=Round-Robin&pdraw=A#1")]
        [InlineData("http://curlingzone.com/event.php?eventid=5327&view=Round-Robin&view=Round-Robin&pdraw=A#1", "http://curlingzone.com/event.php?eventid=5327&view=Round-Robin&view=Round-Robin&pdraw=A#1")]
        [InlineData("http://curlingzone.com", "http://curlingzone.com")]
        public void FormatLinkTest(string link, string expectedLink)
        {
            string newLink = Request.FormatLink(link);
            Xunit.Assert.Equal(expectedLink, newLink);
        }

        [Theory]
        [TestMethod]
        [TestCategory("Game")]
        [InlineData("230195", true, true, "Jones/Laing", "Stirling/Kingan", 9)]
        [InlineData("229847", true, true, "Laurie St-Georges", "Jaimee Gardner", 8)]
        [InlineData("229788", true, true, "Yannick Schwaller", "Ross Paterson", 9)]
        public void GetGame(string czGameId, bool isOverAndFullyParsed, bool isFinal, string team1Name, string team2Name, int currentEnd)
        {
            Console.WriteLine("GetGameTest\nCZGameID: " + czGameId);
            Game g = Parser.GetGame(czGameId, Guid.NewGuid(), Guid.NewGuid());
            Xunit.Assert.Equal(isOverAndFullyParsed, g.IsOverAndFullyParsed);
            Xunit.Assert.Equal(isFinal, g.IsFinal);
            Xunit.Assert.Equal(team1Name, g.Team1.Name);
            Xunit.Assert.Equal(team2Name, g.Team2.Name);
            Xunit.Assert.Equal(currentEnd, g.CurrentEnd);
        }

        [Theory]
        [TestMethod]
        [TestCategory("Team")]
        [InlineData("229788", "Schwaller", 4)]
        [InlineData("230195", "Jones/Laing", 2)]
        public void GetTeam(string czGameId, string teamShortName, int numPlayers)
        {
            Console.WriteLine("GetTeam\nCZGameID: " + czGameId);
            string gameJson = Request.GetGameJson(czGameId);
            Api.GameObject apiGame = Api.GetGameObject(gameJson);
            Team t = Parser.GetTeam(apiGame, true);
            Xunit.Assert.Equal(teamShortName, t.GetTeamShortName());
            Xunit.Assert.Equal(numPlayers, t.Players.Count);
        }


        [Theory]
        [TestMethod]
        [TestCategory("EventFormat")]
        [InlineData("5327", EventFormat.KNOCKOUT)]
        [InlineData("5341", EventFormat.ROUND_ROBIN)]
        [InlineData("5503", EventFormat.ROUND_ROBIN)]
        [InlineData("5326", EventFormat.ROUND_ROBIN)]
        [InlineData("5203", EventFormat.KNOCKOUT)]
        public void GetEventFormat(string czEventId, EventFormat eventFormat)
        {
            Console.WriteLine("GetEventFormatTest\nCZ Event ID: " + czEventId);
            EventFormat ef = Parser.GetEventFormat(czEventId);
            Xunit.Assert.Equal(eventFormat, ef);
        }


        [Theory]
        [TestMethod]
        [TestCategory("Bracket")]
        [InlineData("http://www.curlingzone.com/event.php?eventid=5203&view=Round-Robin&view=Round-Robin&pdraw=A#1", "A", "Parent/Towes")]
        [InlineData("http://www.curlingzone.com/event.php?eventid=5203&view=Round-Robin&view=Round-Robin&pdraw=B#1", "B", "Parent/Towes")]
        [InlineData("http://curlingzone.com/event.php?eventid=5094&view=Round-Robin&view=Round-Robin&pdraw=A#1", "A", "Lisa Menard")]
        [InlineData("http://www.curlingzone.com/event.php?eventid=5198&eventtypeid=81&view=Round-Robin&pdraw=B#1", "B", "Ted Appelman")]
        public void GetBracketFromUrl(string url, string bracketName, string bracketContains)
        {
            Console.WriteLine("GetBracketFromUrlTest\nUrl: " + url);
            Bracket bracket = Parser.GetBracketFromUrl(url);
            Xunit.Assert.Equal(bracketName, bracket.Name);
            Xunit.Assert.True(bracket.Html.Contains(bracketContains));
        }

        [Theory]
        [TestMethod]
        [TestCategory("Standings")]
        [InlineData("5341")]
        [InlineData("5503")]
        [InlineData("5326")]
        public void GetStandingsHtmlTest(string czId)
        {
            string html = Parser.GetStandingsHtml(czId);
            Xunit.Assert.True(Utility.ValidateStandingsHtml(html));
        }


        [Theory]
        [TestMethod]
        [TestCategory("Playoff")]
        [InlineData("5341", false)]
        [InlineData("5503", false)]
        [InlineData("5326", true)]
        [InlineData("5500", false)]
        [InlineData("5000", false)]
        public void GetPlayoffTest(string czId, bool isNull)
        {
            Playoff playoff = Parser.GetPlayoff(czId);
            if (isNull)
            {
                Xunit.Assert.Equal(null, playoff);
                return;
            }
            string html = playoff.Html;
            Xunit.Assert.True(Utility.ValidatePlayoffHtml(html));
        }
    }
}