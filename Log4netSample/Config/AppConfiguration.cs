using System;
using System.IO;
using Microsoft.Extensions.Configuration;

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
                                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }
    }
}
