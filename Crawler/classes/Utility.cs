using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Crawler
{
    public static class Utility
    {
        public static int ParseIntWithDefault(string number, int fallback)
        {
            int x = fallback;
            if (int.TryParse(number, out x))
            {
                return x;
            }
            return fallback;
        }
    }
}

