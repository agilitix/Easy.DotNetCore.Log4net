using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Log4netSample
{
    public static class Log4NetExtensions
    {
        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder, string log4NetConfigFile)
        {
            builder.Services.AddSingleton<ILoggerProvider, Log4NetLoggerProvider>(provider => new Log4NetLoggerProvider(log4NetConfigFile));
            return builder;
        }

        public static ILoggingBuilder AddLog4Net(this ILoggingBuilder builder)
        {
            return builder.AddLog4Net("log4net.config");
        }
    }
}
