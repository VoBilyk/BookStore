namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using Microsoft.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp;
    using BookStore.Shared.DTOs;

    /// <summary>
    /// Page from which started design
    /// </summary>
    public class MainPage
    {
        private readonly ILogger<MainPage> _logger;
        private readonly IAuthService _authService;

        private readonly BookMenuPage _bookPage;
        private readonly ClientMenuPage _clientPage;

        public MainPage(
            IAuthService authService,
            ILogger<MainPage> logger,
            BookMenuPage bookPage,
            ClientMenuPage clientPage)
        {
            this._clientPage = clientPage;
            this._bookPage = bookPage;

            this._authService = authService;
            this._logger = logger;
        }

        public void Run()
        {
            bool run = true;
            while (run)
            {
                var currentClient = _authService.GetCurrentClient();
                if (currentClient != null)
                {
                    Console.WriteLine($"Hello, {currentClient}");
                    Console.WriteLine(new string('-', 30));
                }

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
            if (_authService.GetCurrentClient() != null)
            {
                if (_authService.Logout())
                {
                    _logger.LogInformation("Logout Success");
                }
                else
                {
                    _logger.LogInformation("Can`t logout");
                }

                return;
            }

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter second name: ");
            string secondName = Console.ReadLine();

            var client = new ClientDto
            {
                FirstName = firstName,
                LastName = secondName
            };

            if (_authService.Login(client))
            {
                _logger.LogInformation("Login Success");
            }
            else
            {
                _logger.LogWarning("Client don`t exist");
            }
        }
    }
}
