namespace BookStore.ConsoleApp
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using BookStore.ConsoleApp.MenuPages;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            Startup.ConfigureServices(services);

            // Running app
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(Program));

            try
            {
                serviceProvider.GetService<MainPage>().Display();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, ex.Message);
                NLog.LogManager.Shutdown();
                throw;
            }

            NLog.LogManager.Shutdown();
        }
    }
}
