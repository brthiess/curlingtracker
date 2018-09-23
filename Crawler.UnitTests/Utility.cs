using System;
using System.Threading.Tasks;

namespace Crawler.UnitTests
{
    public class Utility
    {
        public static bool ValidatePlayoffHtml(string html)
        {
            if (!html.Contains("<table")) return false;
            if (!html.Contains("<tr")) return false;
            if (!html.Contains("<td")) return false;
            if (!html.Contains("bracket-info")) return false;
            return true;
        }

        public static bool ValidateStandingsHtml(string html)
        {
            if (!html.Contains("<table")) return false;
            if (!html.Contains("<tr")) return false;
            if (!html.Contains("<td")) return false;
            if (!html.Contains("standings-table")) return false;
            if (!html.Contains("standings-pool-name-data")) return false;
            if (!html.Contains("standings-header")) return false;
            if (!html.Contains("standings-pool-teams-flag")) return false;
            return true;
        }
    }
}