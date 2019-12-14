using System;

namespace MyHomeBar.Logging
{
    // Immutable DTO that contains the log information.
    public class LogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;
        public readonly Exception Exception;
        /// <summary>
        ///  Used for structured logging with message templates (Serilog and NLog)
        /// </summary>
        public readonly object[] Args;

        public LogEntry(LoggingEventType severity, string message, Exception exception = null, params object[] args)
        {
            if (message == null) throw new ArgumentNullException("message");
            if (message == string.Empty) throw new ArgumentException("message cannot be empty", "message");

            Severity = severity;
            Message = message;
            Exception = exception;
            Args = args;
        }
    }
}
