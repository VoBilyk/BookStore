namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using Microsoft.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using Bookstore.ConsoleApp;
    using BookStore.Shared.DTOs;

    /// <summary>
    /// Page from which started design
    /// </summary>
    public class MainPage
    {
        private readonly ILogger<MainPage> _logger;
        private readonly IService<ClientDto> _clientService;

        private readonly BookMenuPage _bookPage;
        private readonly ClientMenuPage _clientPage;

        public MainPage(
            IService<ClientDto> clientService,
            ILogger<MainPage> logger,
            BookMenuPage bookPage,
            ClientMenuPage clientPage)
        {
            this._clientPage = clientPage;
            this._bookPage = bookPage;

            this._clientService = clientService;
            this._logger = logger;
        }

        public void Run()
        {
            bool run = true;
            while (run)
            {
                var mainMenu = new MenuVisualizer();
                mainMenu.Add("Login/Logout", () => LoginLogout())
                    .Add("User menu", () => _clientPage.Run())
                    .Add("Book menu", () => _bookPage.Run())
                    .Add("Exit", () => run = false);

                mainMenu.Display();

                Console.WriteLine();
                Console.WriteLine(new string('-', 30));
            }
        }

        private void LoginLogout()
        {
            if (Startup.CurrentClientId != null)
            {
                Startup.CurrentClientId = null;

                _logger.LogInformation("Logout Success");
                return;
            }

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter second name: ");
            string secondName = Console.ReadLine();

            var client = new ClientDto
            {
                FirstName = firstName,
                SecondName = secondName
            };

            try
            {
                Startup.CurrentClientId = _clientService.Find(client).Id;

                _logger.LogInformation("Login Success");
                return;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
