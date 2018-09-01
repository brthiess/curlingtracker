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
        [InlineData("5295", Gender.Male, "2018-08-30", "2018-09-02", "Oakville Fall Classic", EventType.TeamType.MEN)]
        [InlineData("5422", Gender.Female, "2018-08-31", "2018-09-02", "Carleton Heights Labour Day U21 Spiel", EventType.TeamType.WOMEN)]
        [InlineData("5304", Gender.Mixed, "2018-08-26", "2018-08-30", "Audi quattro Winter Games NZ Mixed Doubles", EventType.TeamType.MIXED_DOUBLES)]
        public void GetEventInfoTest(string czId, Gender g, string startDate, string endDate, string name, EventType.TeamType teamType)
        {
            Event e = Request.GetEventInfo(czId);
            Xunit.Assert.Equal(czId, e.CZId);
            Xunit.Assert.Equal(g, e.Type.Gender);
            Xunit.Assert.Equal(DateTime.Parse(startDate), e.StartDate);
            Xunit.Assert.Equal(DateTime.Parse(endDate), e.EndDate);
            Xunit.Assert.Equal(teamType, e.Type.teamType);
            Xunit.Assert.Equal(name, e.Name);
        }

        [Theory]
        [TestMethod]
        [TestCategory("Game")]
        [InlineData("230195", true, true, "Jones/Laing", "Stirling/Kingan", 9)]
        [InlineData("229847", true, true, "Laurie St-Georges", "Jaimee Gardner", 8)]
        [InlineData("229788", true, true, "Yannick Schwaller", "Ross Paterson", 9)]
        public void GetGame(string czId, bool isOverAndFullyParsed, bool isFinal, string team1Name, string team2Name, int currentEnd)
        {
            Game g = Parser.GetGame(czId, Guid.NewGuid(), Guid.NewGuid());
            //Uncomment in 1 day
            //Assert.Equal(isOverAndFullyParsed, g.IsOverAndFullyParsed);
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
            string gameJson = Request.GetGameJson(czGameId);
            Api.GameObject apiGame = Api.GetGameObject(gameJson);
            Team t = Parser.GetTeam(apiGame, true);
            Xunit.Assert.Equal(teamShortName, t.GetTeamShortName());
            Xunit.Assert.Equal(numPlayers, t.Players.Count);
        }
    }
}