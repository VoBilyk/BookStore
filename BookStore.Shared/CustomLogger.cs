namespace BookStore.Shared
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Shared.Enums;
    using Shared.Interfaces;

    /// <inheritdoc/>
    public class CustomLogger : ICustomLogger
    {
        private static ConcurrentQueue<string> _logQueue;

        private static object _locker;

        private readonly string _name;

        static CustomLogger()
        {
            _logQueue = new ConcurrentQueue<string>();
            _locker = new object();

            // Configurate default settings
            LogFileName = $"{DateTime.Now.ToShortDateString()}.log";
            BufferThreshold = 10;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomLogger"/> class.
        /// </summary>
        /// <param name="loggerName">Logger name</param>
        public CustomLogger(string loggerName)
        {
            this._name = loggerName;
        }

        /// <summary>
        /// Event which notify that log was added to queue
        /// </summary>
        public event EventHandler<LogEventArgs> LogAdded;

        /// <summary>
        /// Gets or sets file name where to write log
        /// </summary>
        public static string LogFileName { get; set; }

        /// <summary>
        /// Gets or sets max buffer threshold after
        /// </summary>
        public static int BufferThreshold { get; set; }

        /// <summary>
        /// Writing logs from queue if there left something
        /// </summary>
        public static void ShutDown()
        {
            if (_logQueue.Any())
            {
                WriteQueue();
            }
        }

        /// <inheritdoc/>
        public void Info(string message)
        {
            Log(LogLevel.Info, message);
        }

        /// <inheritdoc/>
        public void Debug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        /// <inheritdoc/>
        public void Warning(string message, Exception exception = null)
        {
            Log(LogLevel.Warning, message, exception);
        }

        /// <inheritdoc/>
        public void Error(string message, Exception exception = null)
        {
            Log(LogLevel.Error, message, exception);
        }

        /// <inheritdoc/>
        public void Critical(string message, Exception exception = null)
        {
            Log(LogLevel.Critical, message, exception);
        }

        private static void WriteQueue()
        {
            lock (_locker)
            {
                while (_logQueue.TryDequeue(out string message))
                {
                    using (var streamWriter = File.AppendText(LogFileName))
                    {
                        streamWriter.WriteLine(message);
                        streamWriter.Close();
                    }
                }
            }
        }

        private void Log(LogLevel level, string message, Exception ex = null)
        {
            string msg = string.Empty;

            if (ex != null)
            {
                msg = $"time: {DateTime.Now} | level: {level.ToString().ToUpper()} | name: {_name} | message: {ex.GetType().Name} | message: {message}";
            }
            else
            {
                msg = $"time: {DateTime.Now} | level: {level.ToString().ToUpper()} | name: {_name} | message: {message}";
            }

            _logQueue.Enqueue(msg);

            // Notify that log was added to queue
            LogAdded?.Invoke(this, new LogEventArgs(_name, DateTime.Now, level, message, ex));

            if (_logQueue.Count >= BufferThreshold)
            {
                Task.Run(() => WriteQueue());
            }
        }
    }
}