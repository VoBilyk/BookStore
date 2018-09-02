namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using System.Linq;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.Shared.DTOs;
    using BookStore.Shared.Interfaces;
    using BookStore.Shared.Resources;

    /// <summary>
    /// Page for working with books
    /// </summary>
    public class BookMenuPage : IPage
    {
        private readonly ICustomLogger _logger;
        private readonly IMenuVisualizer _menuVisualizer;
        private readonly IOutputEnvironment _outputEnvironment;
        private readonly IClientService _clientService;
        private readonly IAuthService _authService;
        private readonly IBookService _bookService;
        private readonly ICommentService _commentService;
        private readonly IWishListService _wishListService;

        public BookMenuPage(
            ICustomLoggerFactory loggerFactory,
            IMenuVisualizer menuVisualizer,
            IOutputEnvironment outputEnvironment,
            IClientService clientService,
            IAuthService authService,
            IBookService bookService,
            ICommentService commentService,
            IWishListService wishListService)
        {
            this._logger = loggerFactory.CreateLogger<BookMenuPage>();
            this._outputEnvironment = outputEnvironment;
            this._menuVisualizer = menuVisualizer;
            this._clientService = clientService;
            this._authService = authService;
            this._bookService = bookService;
            this._commentService = commentService;
            this._wishListService = wishListService;
        }

        /// <inheritdoc/>
        public void Display()
        {
            var menu = _menuVisualizer.FactoryMethod()
                .Add(Resource.ShowBooks, () => ShowBooks())
                .Add(Resource.Search, () => Find())
                .Add(Resource.AddBook, () => AddBook())
                .Add(Resource.UpdateBook, () => UpdateBook())
                .Add(Resource.RemoveBook, () => RemoveBook())
                .Add(Resource.ReturnBack, () => { });
            menu.Display();
        }

        public void ShowBooks()
        {
            var books = _bookService.GetAll();

            _menuVisualizer.ShowCollection(books);
            _outputEnvironment.Write($"\n{Resource.ChooseSomeone}: ");
            var choice = _outputEnvironment.ReadInt(1, books.Count);

            ShowDetails(books[choice - 1]);
        }

        public void Find()
        {
            _outputEnvironment.Write($"{Resource.Search}: ");
            var query = _outputEnvironment.Read();

            var books = _bookService.Find(query);

            if (!books.Any())
            {
                _outputEnvironment.WriteLine(Resource.NotFound);
                return;
            }

            _menuVisualizer.ShowCollection(books);

            _outputEnvironment.Write($"{Resource.ChooseSomeone}: ");
            var choice = _outputEnvironment.ReadInt(1, books.Count);

            ShowDetails(books[choice - 1]);
        }

        public void ShowDetails(BookDto book)
        {
            _outputEnvironment.WriteLine($"\n{Resource.Name}: {book.Name}");
            _outputEnvironment.WriteLine($"{Resource.Genre}: {book.Genre}");
            _outputEnvironment.WriteLine($"{Resource.Author}: {book.Author}");
            _outputEnvironment.WriteLine($"{Resource.Price}: {book.Price}$");

            _outputEnvironment.WriteLine($"{Resource.UsersWhichWish}:");
            if (!book.WishedClientsId.Any())
            {
                _outputEnvironment.WriteLine($"\t{Resource.NotHave}");
            }

            foreach (var clientId in book.WishedClientsId)
            {
                var client = _clientService.Get(clientId);
                _outputEnvironment.WriteLine($"\t{client}");
            }

            _outputEnvironment.WriteLine($"{Resource.Comments}:");
            var comments = _commentService.GetAll().Where(c => c.BookId == book.Id);
            if (!comments.Any())
            {
                _outputEnvironment.WriteLine($"\t{Resource.NotHave}");
            }

            foreach (var comment in comments)
            {
                _outputEnvironment.WriteLine($"\t{comment}");
            }

            var currentClient = _authService.GetCurrentClient();
            if (currentClient != null)
            {
                UserAction(currentClient, book);
            }

            _outputEnvironment.ReadKey();
        }

        public void UserAction(ClientDto currentClient, BookDto book)
        {
            _outputEnvironment.WriteLine($"{Resource.YourChoice}:");

            var isWished = currentClient.WishedBooksId.Contains(book.Id);
            var isCommented = (from commentId in book.UserCommentsId
                               where currentClient.CommentsId.Contains(commentId)
                               select true).FirstOrDefault();

            var userMenu = _menuVisualizer.FactoryMethod();
            userMenu.Add(isWished ? Resource.RemoveFromWishList : Resource.AddToWishList, () => AddRemoveWishlist(book, isWished))
                    .Add(isCommented ? Resource.RemoveComment : Resource.AddComment, () => AddRemoveComment(book, isCommented))
                    .Add(Resource.ReturnBack, () => { })
                    .Display();
        }

        public void AddRemoveWishlist(BookDto book, bool remove)
        {
            if (remove)
            {
                try
                {
                    var wish = _wishListService.GetAll()
                        .FirstOrDefault(c => c.BookId == book.Id && c.ClientId == _authService.GetCurrentClientId());

                    _wishListService.Delete(wish.Id);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }
            }
            else
            {
                try
                {
                    _wishListService.Create(
                    new WishDto
                    {
                        ClientId = _authService.GetCurrentClientId().Value,
                        BookId = book.Id
                    });
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }
            }
        }

        public void AddRemoveComment(BookDto book, bool remove)
        {
            if (remove)
            {
                try
                {
                    var comment = _commentService.GetAll()
                        .FirstOrDefault(c => c.BookId == book.Id && c.ClientId == _authService.GetCurrentClientId());

                    _commentService.Delete(comment.Id);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }
            }
            else
            {
                try
                {
                    _commentService.Create(
                    new CommentDto
                    {
                        ClientId = _authService.GetCurrentClientId().Value,
                        BookId = book.Id,
                        Text = _outputEnvironment.Read()
                    });
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }
            }
        }

        public void AddBook()
        {
            var book = EnterBookData();

            try
            {
                _bookService.Create(book);
                _outputEnvironment.WriteLine(Resource.CreatedSuccess);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void UpdateBook()
        {
            var books = _bookService.GetAll();
            _menuVisualizer.ShowCollection(books);
            var choice = _outputEnvironment.ReadInt(1, books.Count);

            var book = EnterBookData();

            try
            {
                _bookService.Update(books[choice - 1].Id, book);
                _outputEnvironment.WriteLine(Resource.UpdatedSuccess);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void RemoveBook()
        {
            var books = _bookService.GetAll();

            _menuVisualizer.ShowCollection(books);

            _outputEnvironment.Write($"{Resource.YourChoice}: ");
            var choice = _outputEnvironment.ReadInt(1, books.Count);

            try
            {
                _bookService.Delete(books[choice - 1].Id);
                _outputEnvironment.WriteLine(Resource.DeletedSuccess);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private BookDto EnterBookData()
        {
            _outputEnvironment.Write($"{Resource.EnterNewName}: ");
            var name = _outputEnvironment.Read();

            _outputEnvironment.Write($"{Resource.EnterNewAuthor}: ");
            var author = _outputEnvironment.Read();

            _outputEnvironment.Write($"{Resource.EnterNewGenre}: ");
            var genre = _outputEnvironment.Read();

            _outputEnvironment.Write($"{Resource.EnterNewPrice}: ");
            var price = _outputEnvironment.ReadInt();

            return new BookDto
            {
                Name = name,
                Author = author,
                Genre = genre,
                Price = price
            };
        }
    }
}
