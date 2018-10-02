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
                    news.Image = GetAndDownloadImage(news.Image,  GetWords(news.Title, 2) + "-" + news.PublishDate.ToString("M-dd-yyyy"));
                    //_eventService.AddOrUpdateNews(news)
                }
            }
        }




        private static string GetAndDownloadImage(string imageUrl, string imageName)
        {
            string imageNameWithExtension = StoreImage(imageUrl, imageName);
            return imageNameWithExtension;
        }



        ///Returns image name with extension
        private static string StoreImage(string url, string imageName)
        {
            WebClient webClient = new WebClient();
            string fileExtension = GetFileExtension(url);
            string fullImage = imageName + fileExtension; 
            webClient.DownloadFile(url, Configuration.Values["ImagePath"] + fullImage);
            return fullImage;
        }

        private static string GetFileExtension(string url)
        {
            return System.IO.Path.GetExtension(url);
        }

        private static string GetWords(string input, int count = -1, string[] wordDelimiter = null, StringSplitOptions options = StringSplitOptions.None)
        {
            if (string.IsNullOrEmpty(input)) return "";

            if (count < 0)
                return String.Join(" ", input.Split(wordDelimiter, options));

            string[] words = input.Split(wordDelimiter, count + 1, options);
            if (words.Length <= count)
                return String.Join(" ", words);   // not so many words found

            // remove last "word" since that contains the rest of the string
            Array.Resize(ref words, words.Length - 1);

            return String.Join(" ", words);
        }
    }
}
