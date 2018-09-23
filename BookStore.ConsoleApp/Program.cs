namespace BookStore.ConsoleApp
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    using BookStore.ConsoleApp.MenuPages;
    using BookStore.Shared.Interfaces;
    using BookStore.Shared.Logger;
    using BookStore.Shared.Resources;

    /// <summary>
    /// Entry program class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">Passed arguments to program</param>
        public static void Main(string[] args)
        {
            // Configuration application
            var services = new ServiceCollection();
            Startup.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ICustomLoggerFactory>().CreateLogger(typeof(Program).FullName);

            // It just show how you can to use log event
            logger.LogAdded += (o, e) => Console.WriteLine($"Getting log event with message: {e.Message}\n");

            try
            {
                logger.Debug($"{Resource.ProgramStarted}: {DateTime.Now.ToLocalTime()}");

                // Running application
                serviceProvider.GetService<MainPage>().Display();
            }
            catch (Exception ex)
            {
                logger.Critical(ex.Message, ex);
            }
            finally
            {
                logger.Debug($"{Resource.ProgramFinished}: {DateTime.Now.ToLocalTime()}");
                NLog.LogManager.Shutdown();
                CustomLogger.ShutDown();
            }
        }
    }
}
