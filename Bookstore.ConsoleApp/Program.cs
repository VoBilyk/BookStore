namespace Bookstore.ConsoleApp
{
    using BookStore.ConsoleApp.MenuPages;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            Startup.ConfigureServices(services);

            // Running app
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

            try
            {
                serviceProvider.GetService<MainPage>().Run();
            }
            catch (System.Exception ex)
            {
                logger.LogCritical(ex, ex.Message);
                NLog.LogManager.Shutdown();
                throw;
            }

            NLog.LogManager.Shutdown();
        }
    }
}
