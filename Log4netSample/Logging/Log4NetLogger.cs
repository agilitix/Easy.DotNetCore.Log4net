using System;
using log4net;
using log4net.Core;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Log4netSample.Logging
{
    public class Log4NetLogger : ILogger
    {
        protected readonly ILog _logger;

        public Log4NetLogger(ILog logger)
        {
            _logger = logger;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            string message = formatter(state, exception);

            switch (logLevel)
            {
                case LogLevel.Critical:
                    _logger.Fatal(message, exception);
                    return;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    _logger.Debug(message, exception);
                    return;
                case LogLevel.Error:
                    _logger.Error(message, exception);
                    return;
                case LogLevel.Information:
                    _logger.Info(message, exception);
                    return;
                case LogLevel.Warning:
                    _logger.Warn(message, exception);
                    return;
                default:
                    _logger.Warn($"Out of range log level {logLevel}, writing out as Info.");
                    _logger.Info(message, exception);
                    return;
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return _logger.IsFatalEnabled;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    return _logger.IsDebugEnabled;
                case LogLevel.Error:
                    return _logger.IsErrorEnabled;
                case LogLevel.Information:
                    return _logger.IsInfoEnabled;
                case LogLevel.Warning:
                    return _logger.IsWarnEnabled;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new DisposableScope(_logger, Convert.ToString(state));
        }

        private class DisposableScope : IDisposable
        {
            private readonly ILog _logger;

            public DisposableScope(ILog logger, string s)
            {
                _logger = logger;

                NDC.Push(s);

                if (_logger.Logger.IsEnabledFor(Level.Info))
                {
                    _logger.InfoFormat("BeginScope={0}", s);
                }
            }

            public void Dispose()
            {
                string s = NDC.Pop();

                if (_logger.Logger.IsEnabledFor(Level.Info))
                {
                    _logger.InfoFormat("EndScope={0}", s);
                }
            }
        }
    }
}
