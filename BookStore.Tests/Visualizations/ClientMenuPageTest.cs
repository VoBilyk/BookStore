namespace BookStore.Tests.Visualizations
{
    using System;
    using System.Collections.Generic;
    using FakeItEasy;
    using Microsoft.Extensions.Logging;
    using NUnit.Framework;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.ConsoleApp.MenuPages;
    using BookStore.Shared.DTOs;

    [TestFixture]
    public class ClientMenuPageTest
    {

        private ILogger<ClientMenuPage> _logger;
        private IMenuVisualizer _menuVisualizer;
        private IOutputEnvironment _outputEnvironment;
        private IClientService _clientService;
        private IBookService _bookService;
        private ICommentService _commentService;
        private IWishListService _wishListService;
        private ClientMenuPage _page;

        [SetUp]
        public void Setup()
        {
            this._logger = A.Fake<ILogger<ClientMenuPage>>();
            this._menuVisualizer = A.Fake<IMenuVisualizer>();
            this._outputEnvironment = A.Fake<IOutputEnvironment>();
            this._clientService = A.Fake<IClientService>();
            this._bookService = A.Fake<IBookService>();
            this._commentService = A.Fake<ICommentService>();

            A.CallTo(() => _clientService.GetAll())
                .Returns(new List<ClientDto>
                {
                    new ClientDto
                    {
                        CommentsId = new List<Guid> { Guid.NewGuid() },
                        WishedBooksId = new List<Guid> { Guid.NewGuid() }
                    }
                });

            A.CallTo(() => _outputEnvironment.ReadInt(A<int>._, A<int>._)).Returns(1);

            _page = new ClientMenuPage(
                _logger,
                _menuVisualizer,
                _outputEnvironment,
                _clientService,
                _bookService,
                _commentService);
        }

        [Test]
        public void ShowClients_WhenShowClients_ThenInvokeGetByService()
        {
            // Act
            _page.ShowClients();

            // Assert
            A.CallTo(() => _clientService.GetAll()).MustHaveHappened();
        }

        [Test]
        public void Display_WhenDisplayMenu_ThenCreateMenu()
        {
            // Act
            _page.Display();

            // Assert
            A.CallTo(() => _menuVisualizer.FactoryMethod()).MustHaveHappened();
        }

        [Test]
        public void Find_WhenFindClient_ThenInvokeFindByService()
        {
            // Act
            _page.Find();

            // Assert
            A.CallTo(() => _clientService.Find(A<string>._)).MustHaveHappened();
        }

        [Test]
        public void AddClient_WhenAddClient_ThenInvokeCreateByService()
        {
            // Act
            _page.AddClient();

            // Assert
            A.CallTo(() => _clientService.Create(A<ClientDto>._)).MustHaveHappened();
        }

        [Test]
        public void UpdateClient_WhenUpdateClient_ThenInvokeUpdateByService()
        {
            // Act
            _page.UpdateClient();

            // Assert
            A.CallTo(() => _clientService.Update(A<Guid>._, A<ClientDto>._)).MustHaveHappened();
        }

        [Test]
        public void RemoveClient_WhenRemoveClient_ThenInvokeRemoveByService()
        {
            // Act
            _page.RemoveClient();

            // Assert
            A.CallTo(() => _clientService.Delete(A<Guid>._)).MustHaveHappened();
        }
    }
}