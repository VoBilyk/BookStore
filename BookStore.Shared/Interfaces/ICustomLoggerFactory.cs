namespace BookStore.Shared.Interfaces
{
    /// <summary>
    /// Interface for creating ICustomLogger used class factory
    /// </summary>
    public interface ICustomLoggerFactory
    {
        /// <summary>
        /// Creating custom logger with needed name through generic parameter
        /// </summary>
        /// <typeparam name="TClassName">Class for which need create logger</typeparam>
        /// <returns>Created custom logger</returns>
        ICustomLogger CreateLogger<TClassName>();

        /// <summary>
        /// Creating custom logger with needed name through parameter
        /// </summary>
        /// <param name="loggerName">Custom logger name</param>
        /// <returns>Created custom logger</returns>
        ICustomLogger CreateLogger(string loggerName);
    }
}
