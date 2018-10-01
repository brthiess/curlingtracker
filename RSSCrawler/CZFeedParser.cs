using System;
using CurlingTracker.Models;
using System.Collections.Generic;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;

namespace RSSCrawler
{
    public class CZFeedParser : IFeedParser
    {
        public List<News> GetNewsFeed()
        {
            var newsList = new List<News>();
            string html = Request.GetHtml("http://www.curlingzone.com/news.php");

            HtmlNodeCollection posts = GetPosts(html);

            foreach (var post in posts)
            {
                try
                {
                    string newsId = GetNewsId(post);
                    string imageLink = GetImage(post);
                    string title = GetTitle(post);
                    string link = GetLink(post);
                    string description = GetDescription(post);
                    DateTime publishDate = GetPublishDate(post);
                    var news = new News(newsId, link, title, description, imageLink, publishDate);
                }
                catch (Exception ex)
                {
                    Program.Logger.Log("Error parsing post");
                    Program.Logger.Log(ex);
                }


            }
            return newsList;
        }

        private string GetNewsId(HtmlNode post)
        {

        }

        private string GetImage(HtmlNode post)
        {
            string imageUrl = post.QuerySelectorAll("img").FirstOrDefault().GetAttributeValue("src", "");
            return imageUrl;
        }

        private string GetTitle(HtmlNode post)
        {
            string title = post.QuerySelectorAll(".post-title").FirstOrDefault().InnerText;
            return title;
        }

        private string GetLink(HtmlNode post)
        {
            string link = post.QuerySelectorAll("a").FirstOrDefault().GetAttributeValue("href", "");
            return link;
        }

        private string GetDescription(HtmlNode post)
        {
            string description = post.QuerySelectorAll(".recentcomments").FirstOrDefault().InnerText;
            return description;
        }

        private DateTime GetPublishDate(HtmlNode post)
        {

        }
        private static HtmlNodeCollection GetPosts(string html)
        {
            HtmlNode document = GetHtmlNode(html);

            IEnumerable<HtmlNode> postContainers = document.QuerySelectorAll(".recentposts-widget");
            HtmlNodeCollection posts = null;
            if (postContainers.Count() > 1)
            {
                var htmlNode = postContainers.First();
                posts = htmlNode.ChildNodes;
            }
            return posts;
        }

        private static HtmlNode GetHtmlNode(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc.DocumentNode;
        }
    }

}
