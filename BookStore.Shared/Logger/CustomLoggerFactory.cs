namespace BookStore.Shared.Logger
{
    using BookStore.Shared.Interfaces;

    /// <inheritdoc/>
    public class CustomLoggerFactory : ICustomLoggerFactory
    {
        /// <inheritdoc/>
        public ICustomLogger CreateLogger<TClassName>()
        {
            return new CustomLogger(typeof(TClassName).FullName);
        }

        /// <inheritdoc/>
        public ICustomLogger CreateLogger(string loggerName)
        {
            return new CustomLogger(loggerName);
        }
    }
}
