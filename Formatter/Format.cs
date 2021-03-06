using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Text.RegularExpressions;


namespace Formatter
{
    public class Format
    {
        public static string FormatStandings(string html)
        {

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            //IEnumerable<HtmlNode> links = document.QuerySelectorAll(Config.Values["selectors:homePageCurrentEventIds"]);

            htmlDoc = RemoveBadAttributes(htmlDoc, new List<string> { "style", "width", "height", "align", "border" });
            htmlDoc = Standings.ReplaceClassNames(htmlDoc);
            htmlDoc = Standings.ReplaceImages(htmlDoc);
            htmlDoc = RemoveUnwantedTags(htmlDoc, new List<string> { "b", "a", "font", "br" });
            htmlDoc = Standings.RemoveExtraneousColumns(htmlDoc);
            htmlDoc = Standings.RemoveUnnecessaryHelpText(htmlDoc);
            htmlDoc = Standings.ModifyColumnWidths(htmlDoc);
            htmlDoc = Standings.MoveStuffAround(htmlDoc); //Needs to be 2nd last
            htmlDoc = Standings.AddFinalClasses(htmlDoc);
            return htmlDoc.DocumentNode.OuterHtml;
        }

        public static string FormatBracket(string html, bool isPlayoffBracket = false)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            htmlDoc = Brackets.RenameTags(htmlDoc);
            htmlDoc = Brackets.AddClasses(htmlDoc);
            htmlDoc = Brackets.ModifyBracketNumbers(htmlDoc);
            if (!isPlayoffBracket)
            {
                htmlDoc = RemoveBadAttributes(htmlDoc, new List<string> { "src", "valign", "bgcolor", "style", "width", "height", "align", "border" });
            }
            else 
            {
                htmlDoc = RemoveBadAttributes(htmlDoc, new List<string> { "src", "bgcolor", "style", "border" });
            }
            
            htmlDoc = RemoveUnwantedTags(htmlDoc, new List<string> { "b", "a", "font", "img" });
            htmlDoc = Brackets.ModifyBracketInformation(htmlDoc);

            return htmlDoc.DocumentNode.OuterHtml;
        }

        public static string FormatPlayoff(string html)
        {
            string formattedHtml = FormatBracket(html, true);
            return formattedHtml;
        }

        public static HtmlDocument RemoveBadAttributes(HtmlDocument doc, List<string> badAttributes)
        {
            var tags = doc.DocumentNode.SelectNodes("//*");
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    foreach (var badAttribute in badAttributes)
                    {
                        tag.Attributes.Remove(badAttribute);
                    }
                }
            }
            return doc;
        }


        public static HtmlDocument RemoveUnwantedTags(HtmlDocument document, List<string> unwantedTags)
        {
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