using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Xml;
using CurlingTracker.Models;
using CurlingTracker.Data;
using CurlingTracker.Services;
using Microsoft.EntityFrameworkCore;
using Config;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace RSSCrawler
{
    public class Program
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
            //InitializeDB();
            List<IFeedParser> feeds = new List<IFeedParser>();
            feeds.Add(new CZFeedParser());
            foreach (var feed in feeds)
            {
                List<News> newsList = feed.GetNewsFeed();

                foreach (var news in newsList)
                {
                    news.Image = GetAndDownloadImage(news.Image);
                    //_eventService.AddOrUpdateNews(news)
                }
            }
        }




        private static string GetAndDownloadImage(string imageUrl)
        {
            string imageName = StoreImage(imageUrl);
            return imageName;
        }


       
        ///Returns image name with extension
        private static string StoreImage(string url)
        {
            WebClient webClient = new WebClient();
            Guid imageName = Guid.NewGuid();
            string fileExtension = GetFileExtension(url);
            string fullImage = imageName + fileExtension;
            webClient.DownloadFile(url, Configuration.Values["ImagePath"] + fullImage);
            return fullImage;
        }

        private static string GetFileExtension(string url)
        {
            return System.IO.Path.GetExtension(url);
        }
    }
}
