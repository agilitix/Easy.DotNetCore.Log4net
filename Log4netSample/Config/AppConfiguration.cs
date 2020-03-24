using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Log4netSample.Config
{
    public class AppConfiguration
    {
        public IConfigurationRoot Configuration { get; }
        public string ProcessDirectory => AppDomain.CurrentDomain.BaseDirectory;

        public AppConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }
    }
}
