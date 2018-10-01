using System;
using System.ComponentModel.DataAnnotations;
namespace CurlingTracker.Models
{
    public class News : IPrintable
    {


        public string NewsId {get; set;}

        public string Link {get;set;}

        public string Title {get;set;}

        public string Content {get;set;}

        public string Image {get;set;}

        public DateTime PublishDate {get;set;}
        
        public News(string newsId, string link, string title, string content, string image, DateTime publishDate)
        {
            this.NewsId = newsId;
            this.Link = link;
            this.Title = title;
            this.Content = content;
            this.Image = image;
            this.PublishDate = publishDate;
        }

           public string Print()
        {
            string resultString = this.Title + ": " + this.PublishDate.ToString();
            return resultString;
        }
    }
}