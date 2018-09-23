namespace BookStore.Shared.Logger
{
    using System;

    using BookStore.Shared.Enums;

    /// <summary>
    /// Class that implement EventArgs to be send to event
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventArgs"/> class.
        /// </summary>
        /// <param name="name">Log event name</param>
        /// <param name="time">Created log time</param>
        /// <param name="level">Log level</param>
        /// <param name="message">Log message</param>
        /// <param name="exception">Log exception</param>
        public LogEventArgs(string name, DateTime time, LogLevel level, string message, Exception exception)
        {
            Name = name;
            Time = time;
            Level = level;
            Message = message;
            Exception = exception;
        }

        /// <summary>
        /// Gets or sets event log name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets event log time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets event log level
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// Gets or sets event log message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets event log exception
        /// </summary>
        public Exception Exception { get; set; }
    }
}
