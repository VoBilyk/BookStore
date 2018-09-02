namespace BookStore.Shared.Interfaces
{
    /// <summary>
    /// Interface for creating ICustomLogger used class factory
    /// </summary>
    public interface ICustomLoggerFactory
    {
        /// <summary>
        /// Creating custom logger with needed name through generic parametr
        /// </summary>
        /// <typeparam name="TClassName">Class for which need create logger</typeparam>
        /// <returns>Created castom logger</returns>
        ICustomLogger CreateLogger<TClassName>();

        /// <summary>
        /// Creating custom logger with needed name through parametr
        /// </summary>
        /// <param name="loggerName">Custom logger name</param>
        /// <returns>Created castom logger</returns>
        ICustomLogger CreateLogger(string loggerName);
    }
}
