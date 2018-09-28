using System;
using System.ServiceModel;
using System.Xml;

namespace RSSCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://fooblog.com/feed";
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                String subject = item.Title.Text;
                String summary = item.Summary.Text;
            }
        }
    }
}
