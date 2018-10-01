using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using CurlingTracker.Models;
using System.Linq;
using System.Threading;

namespace RSSCrawler
{
    public static class Request
    {
        public static string GetHtml(string url)
        {
            url = (url.LastIndexOf("#") > 0 ? url.Substring(0, url.LastIndexOf("#")) : url);
            int attemptNumber = 1;
            while (attemptNumber <= 3)
            {
                Program.Logger.Log("Attempt #" + attemptNumber + " to get URL: " + url);
                int crawlDelayInMilliseconds = int.Parse(Configuration.Values["misc:crawlDelay"]) * 1000;
                Thread.Sleep(crawlDelayInMilliseconds);
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        string htmlCode = client.DownloadString(url);
                        return htmlCode;
                    }
                }
                catch (Exception ex)
                {
                    if (attemptNumber == 3)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                attemptNumber++;
            }
            return "";
        }
    }
}