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
            //string newBracketHtml = Format.FormatBracket(File.ReadAllText("TestHtml/Bracket-A.html"));
            //File.WriteAllText("TestHtml/Updated-Bracket.html", newBracketHtml);

            string newBracketHtml = Format.FormatPlayoff(File.ReadAllText("TestHtml/Playoff.html"));
            File.WriteAllText("TestHtml/Updated-Playoff.html", newBracketHtml);

            //string newStandingsHtml = FormatStandings(File.ReadAllText("TestHtml/Standings.html"));
            //File.WriteAllText("TestHtml/Updated-Standings.html", newStandingsHtml);
        }

    }
}
