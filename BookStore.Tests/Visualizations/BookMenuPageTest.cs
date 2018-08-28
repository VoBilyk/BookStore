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
    public class BookMenuPageTest
    {

        private ILogger<BookMenuPage> _logger;
        private IMenuVisualizer _menuVisualizer;
        private IOutputEnvironment _outputEnvironment;
        private IClientService _clientService;
        private IAuthService _authService;
        private IBookService _bookService;
        private ICommentService _commentService;
        private IWishListService _wishListService;
        private BookMenuPage _page;

        [SetUp]
        public void Setup()
        {
            this._logger = A.Fake<ILogger<BookMenuPage>>();
            this._menuVisualizer = A.Fake<IMenuVisualizer>();
            this._outputEnvironment = A.Fake<IOutputEnvironment>();
            this._clientService = A.Fake<IClientService>();
            this._authService = A.Fake<IAuthService>();
            this._bookService = A.Fake<IBookService>();
            this._commentService = A.Fake<ICommentService>();
            this._wishListService = A.Fake<IWishListService>();

            A.CallTo(() => _bookService.GetAll())
                .Returns(new List<BookDto>
                {
                    new BookDto
                    {
                        UserCommentsId = new List<Guid> { Guid.NewGuid() },
                        WishedClientsId = new List<Guid> { Guid.NewGuid() }
                    }
                });
            A.CallTo(() => _outputEnvironment.ReadInt(A<int>._, A<int>._)).Returns(1);

            _page = new BookMenuPage(
                _logger,
                _menuVisualizer,
                _outputEnvironment,
                _clientService,
                _authService,
                _bookService,
                _commentService,
                _wishListService);
        }

        [Test]
        public void ShowBooks_WhenShowBooks_ThenInvokeGetByService()
        {
            // Act
            _page.ShowBooks();

            // Assert
            A.CallTo(() => _bookService.GetAll()).MustHaveHappened();
        }

        [Test]
        public void Find_WhenFindBook_ThenInvokeFindByService()
        {
            // Act
            _page.Find();

            // Assert
            A.CallTo(() => _bookService.Find(A<string>._)).MustHaveHappened();
        }

        [Test]
        public void Display_WhenDisplayMenu_ThenCreateMenu()
        {
            // Arrange
            A.CallTo(() => _menuVisualizer.FactoryMethod()).Returns(_menuVisualizer);
            A.CallTo(() => _menuVisualizer.Add(A<string>._, A<Action>._)).Returns(_menuVisualizer);

            // Act
            _page.Display();

            // Assert
            A.CallTo(() => _menuVisualizer.FactoryMethod()).MustHaveHappened();
        }

        [Test]
        public void AddBook_WhenAddBook_ThenInvokeCreateByService()
        {
            // Act
            _page.AddBook();

            // Assert
            A.CallTo(() => _bookService.Create(A<BookDto>._)).MustHaveHappened();
        }

        [Test]
        public void UpdateBook_WhenUpdateBook_ThenInvokeUpdateByService()
        {
            // Act
            _page.UpdateBook();

            // Assert
            A.CallTo(() => _bookService.Update(A<Guid>._, A<BookDto>._)).MustHaveHappened();
        }

        [Test]
        public void RemoveBook_WhenRemoveBook_ThenInvokeRemoveByService()
        {
            // Act
            _page.RemoveBook();

            // Assert
            A.CallTo(() => _bookService.Delete(A<Guid>._)).MustHaveHappened();
        }

        [Test]
        public void CreateWish_WhenCreateWish_ThenInvokeCreateByService()
        {
            // Arrange
            var book = new BookDto { Id = Guid.NewGuid() };
            A.CallTo(() => _authService.GetCurrentClientId()).Returns(Guid.NewGuid());

            // Act
            _page.AddRemoveWishlist(book, false);

            // Assert
            A.CallTo(() => _wishListService.Create(A<WishDto>._)).MustHaveHappened();
        }

        [Test]
        public void RemoveWish_WhenRemoveWish_ThenInvokeRemoveByService()
        {
            // Arrange
            var book = new BookDto { Id = Guid.NewGuid() };
            var userId = Guid.NewGuid();
            A.CallTo(() => _authService.GetCurrentClientId()).Returns(userId);

            A.CallTo(() => _wishListService.GetAll())
                .Returns(new List<WishDto> { new WishDto { Id = Guid.NewGuid(), BookId = book.Id, ClientId = userId } });

            // Act
            _page.AddRemoveWishlist(book, true);

            // Assert
            A.CallTo(() => _wishListService.Delete(A<Guid>._)).MustHaveHappened();
        }

        [Test]
        public void CreateComment_WhenCreateComment_ThenInvokeCreateByService()
        {
            // Arrange
            var book = new BookDto { Id = Guid.NewGuid() };
            A.CallTo(() => _authService.GetCurrentClientId()).Returns(Guid.NewGuid());

            // Act
            _page.AddRemoveComment(book, false);

            // Assert
            A.CallTo(() => _commentService.Create(A<CommentDto>._)).MustHaveHappened();
        }

        [Test]
        public void RemoveComment_WhenRemoveComment_ThenInvokeRemoveByService()
        {
            // Arrange
            var book = new BookDto { Id = Guid.NewGuid() };
            var userId = Guid.NewGuid();
            A.CallTo(() => _authService.GetCurrentClientId()).Returns(userId);

            A.CallTo(() => _commentService.GetAll())
                .Returns(new List<CommentDto> { new CommentDto { Id = Guid.NewGuid(), BookId = book.Id, ClientId = userId } });

            // Act
            _page.AddRemoveComment(book, true);

            // Assert
            A.CallTo(() => _commentService.Delete(A<Guid>._)).MustHaveHappened();
        }
    }
}