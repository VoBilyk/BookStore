namespace BookStore.Shared.Interfaces
{
    using System;

    /// <summary>
    /// Interface for customlogger
    /// </summary>
    public interface ICustomLogger
    {
        /// <summary>
        /// Event which notify that log was added to queue
        /// </summary>
        event EventHandler<LogEventArgs> LogAdded;

        /// <summary>
        /// Logging some information message
        /// </summary>
        /// <param name="message">Log message</param>
        void Info(string message);

        /// <summary>
        /// Logging some debug message
        /// </summary>
        /// <param name="message">Log message</param>
        void Debug(string message);

        /// <summary>
        /// Logging some warning message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Optional: exception which need to log</param>
        void Warning(string message, Exception exception = null);

        /// <summary>
        /// Logging some warning message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Optional: exception which need to log</param>
        void Error(string message, Exception exception = null);

        /// <summary>
        /// Logging some critical message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Optional: exception which need to log</param>
        void Critical(string message, Exception exception = null);
    }
}
