using System;
using System.Collections.Generic;
using System.Text;

namespace MyHomeBar.Logging
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Information,
                message, null));
        }

        public static void Log(this ILogger logger, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Error,
                exception.Message, exception));
        }

        // More methods here.
    }
}
