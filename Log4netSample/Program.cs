﻿using System;
using Log4netSample.Config;
using Log4netSample.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Log4netSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read log4net config file name.
            AppConfiguration appConfig = new AppConfiguration();
            IConfigurationSection sectionLog4net = appConfig.Configuration.GetSection("log4net");
            string log4netConfig = sectionLog4net.GetValue<string>("configFile");

            // Create the log4net logger factory.
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddLog4Net(log4netConfig); });

            // Get a logger from factory.
            ILogger logger = loggerFactory.CreateLogger(nameof(Program));

            // Log some infos.
            logger.Log(LogLevel.Information, "An info message.");
            logger.LogInformation(new Exception("Something wrong happened"), "Another info message");

            Console.WriteLine();
            Console.Write("Hit a key to exit:");
            Console.ReadKey();
        }
    }
}
