﻿using System;
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
            string newStandingsHtml = FormatStandings(File.ReadAllText("TestHtml/Standings.html"));
            File.WriteAllText("TestHtml/Updated-Standings.html", newStandingsHtml);
        }
        public static string FormatStandings(string html)
        {

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            //IEnumerable<HtmlNode> links = document.QuerySelectorAll(Config.Values["selectors:homePageCurrentEventIds"]);

            htmlDoc = RemoveBadAttributes(htmlDoc);
            htmlDoc = ReplaceClassNames(htmlDoc);
            htmlDoc = ReplaceImages(htmlDoc);
            htmlDoc = RemoveUnwantedTags(htmlDoc);
            htmlDoc = RemoveExtraneousColumns(htmlDoc);
            htmlDoc = RemoveUnnecessaryHelpText(htmlDoc);
            htmlDoc = ModifyColumnWidths(htmlDoc);
            htmlDoc = MoveStuffAround(htmlDoc); //Needs to be 2nd last
            htmlDoc = AddFinalClasses(htmlDoc);
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
                    tag.Attributes.Remove("border");
                }
            }
            return doc;
        }

        public static HtmlDocument ReplaceClassNames(HtmlDocument htmlDoc)
        {
            var classesToReplace = new List<Tuple<string, string>>();
            classesToReplace.Add(Tuple.Create(".menu-blue", "standings-header"));
            classesToReplace.Add(Tuple.Create(".tourLarge.tourTitle", "standings-pool-name-row"));
            classesToReplace.Add(Tuple.Create("span.tourLarge", "standings-pool-team-name"));
            classesToReplace.Add(Tuple.Create(".tourWatermark.tourMedium.tourNoBold", "standings-pool-team-location"));
            classesToReplace.Add(Tuple.Create(".tourBold.tourData1", "standings-pool-teams-row"));
            classesToReplace.Add(Tuple.Create("td.tourLarge", "standings-pool-stat"));
            classesToReplace.Add(Tuple.Create(".tourEndCell", "standings-pool-teams-flag"));
            classesToReplace.Add(Tuple.Create("td:nth-child(2).tourLeft", "standings-pool-team"));
            classesToReplace.Add(Tuple.Create(".tourFinalText", "standings-pool-name"));
            classesToReplace.Add(Tuple.Create(".tourLeft", "standings-pool-name-data"));
            classesToReplace.Add(Tuple.Create(".chk-text", "standings-table"));
            foreach (var classToReplace in classesToReplace)
            {
                IEnumerable<HtmlNode> classes = htmlDoc.DocumentNode.QuerySelectorAll(classToReplace.Item1);
                foreach (HtmlNode node in classes)
                {
                    node.SetAttributeValue("class", classToReplace.Item2);
                }
            }

            return htmlDoc;
        }

        public static HtmlDocument ReplaceImages(HtmlDocument htmlDoc)
        {
            var imagesToReplace = new List<Tuple<string, string>>();
            imagesToReplace.Add(Tuple.Create("236_flag", "/images/flags/alberta.png"));
            imagesToReplace.Add(Tuple.Create("curlingzone", "/images/flags/unknown.png"));//IMPORTANT: This should always be the last one added.
            foreach (var classToReplace in imagesToReplace)
            {
                IEnumerable<HtmlNode> classes = htmlDoc.DocumentNode.QuerySelectorAll("img[src*=\"" + classToReplace.Item1 + "\"]");
                foreach (HtmlNode node in classes)
                {
                    node.SetAttributeValue("src", classToReplace.Item2);
                }
            }
            return htmlDoc;
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
            unwantedTags.Add("a");

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


        public static HtmlDocument ModifyColumnWidths(HtmlDocument htmlDoc)
        {
            var poolNameHeaders = htmlDoc.DocumentNode.QuerySelectorAll(".standings-pool-name-data");
            foreach(var poolNameHeader in poolNameHeaders)
            {
                poolNameHeader.SetAttributeValue("colspan", "2");
            }
            return htmlDoc;
        }

        public static HtmlDocument MoveStuffAround(HtmlDocument htmlDoc)
        {

            IEnumerable<HtmlNode> poolRows = htmlDoc.DocumentNode.QuerySelectorAll(".standings-pool-name-row");
            foreach (HtmlNode poolRow in poolRows)
            {
                IEnumerable<HtmlNode> headers = htmlDoc.DocumentNode.QuerySelectorAll(".standings-header");
                foreach (HtmlNode header in headers)
                {
                    if (!header.InnerHtml.ToLower().Contains("team"))
                    {
                        poolRow.AppendChild(header);
                    }
                }
            }
            var unwantedRow = htmlDoc.DocumentNode.SelectNodes("//tr[1]").First();
            unwantedRow.Remove();
            return htmlDoc;
        }
        public static HtmlDocument AddFinalClasses(HtmlDocument htmlDoc)
        {
            IEnumerable<HtmlNode> poolRows = htmlDoc.DocumentNode.QuerySelectorAll(".standings-pool-name-data");
            foreach (HtmlNode poolRow in poolRows)
            {
                poolRow.SetAttributeValue("class", poolRow.GetAttributeValue("class", "") + " standings-header");
            }
            return htmlDoc;
        }

        private static HtmlDocument RemoveExtraneousColumns(HtmlDocument htmlDoc)
        {
            htmlDoc = RemoveExtraneousColumnsAfterColumn(htmlDoc, 5);

            return htmlDoc;
        }

        private static HtmlDocument RemoveExtraneousColumnsAfterColumn(HtmlDocument htmlDoc, int columnNumber)
        {
            var unwantedColumns = htmlDoc.DocumentNode.SelectNodes("//td[" + columnNumber.ToString() + "]");
            while (unwantedColumns != null && unwantedColumns.Count() > 0)
            {
                foreach (var unwantedColumn in unwantedColumns)
                {
                    unwantedColumn.Remove();
                }
                unwantedColumns = htmlDoc.DocumentNode.SelectNodes("//td[" + columnNumber.ToString() + "]");
            }

            return htmlDoc;
        }

        private static HtmlDocument RemoveUnnecessaryHelpText(HtmlDocument htmlDoc)
        {
            var unnecessaryText = htmlDoc.DocumentNode.SelectNodes("//*[text()[contains(., 'GP = Games Played')]]");
            if (unnecessaryText != null)
            {
                foreach (var text in unnecessaryText)
                {
                    text.Remove();
                }
            }
            return htmlDoc;
        }
    }
}
