using System;
using log4net.Util;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Log4netSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the log4net logger factory.
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddLog4Net("log4net.config"); });

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
