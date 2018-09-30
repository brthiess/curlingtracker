﻿using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Xml;
using CurlingTracker.Models;
using CurlingTracker.Data;
using CurlingTracker.Services;
using Microsoft.EntityFrameworkCore;
using Config;
using Microsoft.Extensions.Configuration;


namespace RSSCrawler
{
    class Program
    {

        public static CurlingTracker.ILogger Logger = new CurlingTracker.ConsoleLogger();
        private static FakeDBEventService _eventService;
        private static void InitializeDB()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CurlingContext>();
            optionsBuilder.UseSqlite(Configuration.Values.GetConnectionString("DefaultConnection"));
            Logger.Log("Connecting to DB with connection string: " + Configuration.Values.GetConnectionString("DefaultConnection"));
            var context = new CurlingContext(Configuration.Values.GetConnectionString("DefaultConnection"), optionsBuilder.Options);
            _eventService = new FakeDBEventService(context);
            Logger.Log(_eventService.ToString());
        }


        static void Main(string[] args)
        {
            InitializeDB();
            var feeds = new List<Feed>(
                new Feed() { Name = "CZ", Type = FeedType.RSS, Url = "http://www.curlingzone.com/talk/?feed=atom" }
            );
            FeedParser parser = new FeedParser();
            foreach (var feed in feeds)
            {
                var items = parser.Parse(feed.Url, FeedType.Atom);
                foreach (var item in items)
                {
                    News n = new News(item.Link, item.Title, item.Content, null, item.PublishDate);
                }
            }

        }

        private static GetLink(string link)
        {
            
        }
    }
}