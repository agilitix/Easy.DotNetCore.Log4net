using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.Extensions.Logging;

namespace Log4netSample.Logging
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();
        private readonly ILoggerRepository _loggerRepository;

        public Log4NetLoggerProvider(string log4netConfig)
        {
            _loggerRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(_loggerRepository, new FileInfo(log4netConfig));
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName,
                                     name =>
                                     {
                                         ILog logger = LogManager.GetLogger(_loggerRepository.Name, name);
                                         return new Log4NetLogger(logger);
                                     });
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
