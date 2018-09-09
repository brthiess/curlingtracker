using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Text.RegularExpressions;


namespace Formatter
{
    class Program
    {

        static void Main(string[] args)
        {
            FormatStandings(File.ReadAllText("TestHtml/Standings.html"));
        }
        public static string FormatStandings(string html)
        {

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            //IEnumerable<HtmlNode> links = document.QuerySelectorAll(Config.Values["selectors:homePageCurrentEventIds"]);

            htmlDoc = RemoveBadAttributes(htmlDoc);
            htmlDoc = ReplaceClassNames(htmlDoc);
            htmlDoc = RemoveUnwantedTags(htmlDoc);
            htmlDoc = RemoveExtraneousColumns(htmlDoc);
            return htmlDoc.DocumentNode.OuterHtml;
        }

        public static HtmlDocument RemoveBadAttributes(HtmlDocument doc)
        {
            var tags = doc.DocumentNode.SelectNodes("//*");
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    tag.Attributes.Remove("style");
                    tag.Attributes.Remove("width");
                    tag.Attributes.Remove("height");
                    tag.Attributes.Remove("align");
                }
            }
            return doc;
        }

        public static HtmlDocument ReplaceClassNames(HtmlDocument doc)
        {
            IEnumerable<HtmlNode> classes = doc.DocumentNode.QuerySelectorAll(".menu-blue");
            foreach (HtmlNode node in classes)
            {
                node.SetAttributeValue("class", "standings-header");
            }
            return doc;
        }

        private static HtmlNode GetHtmlNode(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc.DocumentNode;
        }

        public static HtmlDocument RemoveUnwantedTags(HtmlDocument document)
        {

            var unwantedTags = new List<String>();
            unwantedTags.Add("b");
            unwantedTags.Add("br");

            HtmlNodeCollection tryGetNodes = document.DocumentNode.SelectNodes("./*|./text()");

            if (tryGetNodes == null || !tryGetNodes.Any())
            {
                return document;
            }

            var nodes = new Queue<HtmlNode>(tryGetNodes);

            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                var parentNode = node.ParentNode;

                var childNodes = node.SelectNodes("./*|./text()");

                if (childNodes != null)
                {
                    foreach (var child in childNodes)
                    {
                        nodes.Enqueue(child);
                    }
                }

                if (unwantedTags.Any(tag => tag == node.Name))
                {
                    if (childNodes != null)
                    {
                        foreach (var child in childNodes)
                        {
                            parentNode.InsertBefore(child, node);
                        }
                    }

                    parentNode.RemoveChild(node);

                }
            }

            return document;
        }
    }
}
