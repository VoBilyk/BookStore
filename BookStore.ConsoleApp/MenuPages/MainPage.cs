namespace BookStore.ConsoleApp.MenuPages
{
    using Microsoft.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.Shared.DTOs;
    using BookStore.Shared;
    using BookStore.Shared.Resources;

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
                    _outputEnvironment.WriteLine($"{Resource.Hello}, {currentClient}");
                    _outputEnvironment.WriteLine(new string('-', 30));
                }

                var mainMenu = _menuVisualizer.FactoryMethod();
                mainMenu.Add(_authService.GetCurrentClientId().HasValue ? Resource.Logout : Resource.Login, () => LoginLogout())
                    .Add(Resource.ClientMenu, () => _clientPage.Display())
                    .Add(Resource.BookMenu, () => _bookPage.Display())
                    .Add(Resource.SwitchLanguage, () => LanguageSwitcher.Switch())
                    .Add(Resource.Exit, () => run = false);

                mainMenu.Display();

                _outputEnvironment.WriteLine(new string('-', 30));
            }
        }

        public void LoginLogout()
        {
            if (_authService.GetCurrentClient() != null)
            {
                if (_authService.Logout())
                {
                    _logger.LogInformation(Resource.LogoutSuccess);
                }
                else
                {
                    _logger.LogInformation(Resource.CannotLogout);
                }

                return;
            }

            _outputEnvironment.Write($"{Resource.EnterFirstName}: ");
            var firstName = _outputEnvironment.Read();

            _outputEnvironment.Write($"{Resource.EnterLastName}: ");
            var lastName = _outputEnvironment.Read();

            var client = new ClientDto
            {
                FirstName = firstName,
                LastName = lastName
            };

            if (_authService.Login(client))
            {
                _logger.LogInformation(Resource.LoginSuccess);
            }
            else
            {
                _logger.LogWarning(Resource.ClientNotExist);
            }
        }
    }
}
