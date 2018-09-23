namespace BookStore.Tests.Visualizations
{
    using System;
    using FakeItEasy;
    using NUnit.Framework;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.ConsoleApp.MenuPages;
    using BookStore.Shared.DTOs;
    using BookStore.Shared.Interfaces;

    [TestFixture]
    public class MainMenuPageTest
    {
        private ICustomLoggerFactory _logger;
        private IMenuVisualizer _menuVisualizer;
        private IOutputEnvironment _outputEnvironment;
        private IAuthService _authService;
        private MainPage _page;
        private IBookPage _bookPage;
        private IClientPage _clientPage;
        private ISettingsPage _settingsPage;

        [SetUp]
        public void Setup()
        {
            this._logger = A.Fake<ICustomLoggerFactory>();
            this._menuVisualizer = A.Fake<IMenuVisualizer>();
            this._outputEnvironment = A.Fake<IOutputEnvironment>();
            this._authService = A.Fake<IAuthService>();
            this._bookPage = A.Fake<IBookPage>();
            this._clientPage = A.Fake<IClientPage>();
            this._settingsPage = A.Fake<ISettingsPage>();

            _page = new MainPage(
                _logger,
                _menuVisualizer,
                _outputEnvironment,
                _authService,
                _bookPage,
                _clientPage,
                _settingsPage);
        }

        [Test]
        public void Logout_WhenCurrentUserExist_ThenInvokeLogout()
        {
            // Arrange
            A.CallTo(() => _authService.GetCurrentClient())
                .Returns(new ClientDto { Id = Guid.NewGuid() });

            // Act
            _page.LoginLogout();

            // Assert
            A.CallTo(() => _authService.Logout()).MustHaveHappened();
        }

        [Test]
        public void Login_WhenNotAuth_ThenLogin()
        {
            // Arrange
            A.CallTo(() => _authService.GetCurrentClient())
                .Returns(null);

            // Act
            _page.LoginLogout();

            // Assert
            A.CallTo(() => _authService.Login(A<ClientDto>._)).MustHaveHappened();
        }
    }
}