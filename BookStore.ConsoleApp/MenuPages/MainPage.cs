namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using Microsoft.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.Shared.DTOs;

    /// <summary>
    /// Page from which started design
    /// </summary>
    public class MainPage : IPage
    {
        private readonly ILogger<MainPage> _logger;
        private readonly IAuthService _authService;
        private readonly IMenuVisualizer _menuVisualizer;
        private readonly IOutputEnvironment _outputEnvironment;

        private readonly BookMenuPage _bookPage;
        private readonly ClientMenuPage _clientPage;

        public MainPage(
            ILogger<MainPage> logger,
            IMenuVisualizer menuVisualizer,
            IOutputEnvironment outputEnvironment,
            IAuthService authService,
            BookMenuPage bookPage,
            ClientMenuPage clientPage)
        {
            this._clientPage = clientPage;
            this._bookPage = bookPage;

            this._menuVisualizer = menuVisualizer;
            this._outputEnvironment = outputEnvironment;
            this._authService = authService;
            this._logger = logger;
        }

        /// <inheritdoc/>
        public void Display()
        {
            bool run = true;
            while (run)
            {
                var currentClient = _authService.GetCurrentClient();
                if (currentClient != null)
                {
                    _outputEnvironment.WriteLine($"Hello, {currentClient}");
                    _outputEnvironment.WriteLine(new string('-', 30));
                }

                var mainMenu = _menuVisualizer.FactoryMethod();
                mainMenu.Add("Login/Logout", () => LoginLogout())
                    .Add("User menu", () => _clientPage.Display())
                    .Add("Book menu", () => _bookPage.Display())
                    .Add("Exit", () => run = false);

                mainMenu.Display();

                _outputEnvironment.WriteLine("\n");
                _outputEnvironment.WriteLine(new string('-', 30));
            }
        }

        public void LoginLogout()
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

            _outputEnvironment.Write("Enter first name: ");
            var firstName = _outputEnvironment.Read();

            _outputEnvironment.Write("Enter second name: ");
            var secondName = _outputEnvironment.Read();

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
