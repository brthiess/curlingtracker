using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Crawler
{
    public static class Config
    {
        private static IConfigurationRoot _config = null;
        public static IConfigurationRoot Values
        {
            get 
            {   
                if (_config != null)
                {
                    return _config;
                }
                IConfigurationBuilder builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                _config = builder.Build();  
                return _config;
            }
        }
    }
}

