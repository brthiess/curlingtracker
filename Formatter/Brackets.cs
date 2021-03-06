using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Text.RegularExpressions;


namespace Formatter
{
    public static class Brackets
    {
        public static HtmlDocument RenameTags(HtmlDocument htmlDoc)
        {
            var teams = htmlDoc.DocumentNode.QuerySelectorAll("font");
            foreach (var team in teams)
            {
                team.Name = "div";
            }
            return htmlDoc;
        }

        public static HtmlDocument AddClasses(HtmlDocument htmlDoc)
        {
            var classesToAdd = new List<Tuple<string, string>>();
            classesToAdd.Add(Tuple.Create("[bgcolor=\"#0000CC\"][rowspan]", "vertical-bracket-line bracket-line"));
            classesToAdd.Add(Tuple.Create("[bgcolor=\"#0000CC\"][colspan]", "horizontal-bracket-line bracket-line"));
            classesToAdd.Add(Tuple.Create("[colspan=\"2\"]", "bracket-team-container"));
            classesToAdd.Add(Tuple.Create(".date", "bracket-info"));
            classesToAdd.Add(Tuple.Create("div[align=right]", "bracket-info"));
            classesToAdd.Add(Tuple.Create(".teams", "bracket-team"));
            classesToAdd.Add(Tuple.Create(".brackets", "bracket"));
            foreach (var classToAdd in classesToAdd)
            {
                IEnumerable<HtmlNode> classes = htmlDoc.DocumentNode.QuerySelectorAll(classToAdd.Item1);
                foreach (HtmlNode node in classes)
                {
                    if (classToAdd.Item1 == "div[align=right]") //Only add bracket-info class to align=right if bracket-info has not been already added
                    {
                        if (!node.InnerHtml.Contains("bracket-info"))
                        {
                            node.SetAttributeValue("class", classToAdd.Item2);
                        }
                    }
                    else
                    {
                        node.SetAttributeValue("class", classToAdd.Item2);
                    }
                }
            }
            return htmlDoc;
        }

        public static HtmlDocument ModifyBracketNumbers(HtmlDocument htmlDoc)
        {
            IEnumerable<HtmlNode> tds = htmlDoc.DocumentNode.QuerySelectorAll("td");
            foreach (var td in tds)
            {
                if (td.InnerHtml.Contains("[") && td.InnerHtml.Contains("]"))
                {
                    td.SetAttributeValue("class", "bracket-number");
                }
            }
            return htmlDoc;
        }

        public static HtmlDocument ModifyBracketInformation(HtmlDocument htmlDoc)
        {
            IEnumerable<HtmlNode> bracketInfos = htmlDoc.DocumentNode.QuerySelectorAll(".bracket-info");
            foreach (var bracketInfo in bracketInfos)
            {
                string[] bracketInfoSplit = bracketInfo.InnerHtml.Split("<br>");
                string modifiedBracketInfo = "";
                foreach (string bracketInfoPart in bracketInfoSplit)
                {
                    modifiedBracketInfo += "<span class=\"bracket-info-part\">" + bracketInfoPart + "</span>";
                }
                bracketInfo.InnerHtml = modifiedBracketInfo;
            }
            return htmlDoc;
        }
    }
}