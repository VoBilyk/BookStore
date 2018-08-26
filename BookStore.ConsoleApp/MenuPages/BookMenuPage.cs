namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.Shared.DTOs;

    /// <summary>
    /// Page for working with books
    /// </summary>
    public class BookMenuPage : IPage
    {
        private readonly ILogger<BookMenuPage> _logger;
        private readonly IMenuVisualizer _menuVisualizer;
        private readonly IOutputEnvironment _outputEnvironment;
        private readonly IClientService _clientService;
        private readonly IAuthService _authService;
        private readonly IBookService _bookService;
        private readonly ICommentService _commentService;
        private readonly IWishListService _wishListService;

        public BookMenuPage(
            ILogger<BookMenuPage> logger,
            IMenuVisualizer menuVisualizer,
            IOutputEnvironment outputEnvironment,
            IClientService clientService,
            IAuthService authService,
            IBookService bookService,
            ICommentService commentService,
            IWishListService wishListService)
        {
            this._logger = logger;
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
                .Add("Show books", () => ShowBooks())
                .Add("Add book", () => AddBook())
                .Add("Update book", () => UpdateBook())
                .Add("Remove book", () => RemoveBook())
                .Add("Return back", () => { });
            menu.Display();
        }

        public void ShowBooks()
        {
            var books = _bookService.GetAll();

            _menuVisualizer.ShowCollection(books);
            _outputEnvironment.Write("\nChoose someone: ");
            var choice = _outputEnvironment.ReadInt(1, books.Count);

            ShowDetails(books[choice - 1]);
        }

        public void ShowDetails(BookDto book)
        {
            _outputEnvironment.WriteLine($"\nName: {book.Name}");
            _outputEnvironment.WriteLine($"Genre: {book.Genre}");
            _outputEnvironment.WriteLine($"Author: {book.Author}");
            _outputEnvironment.WriteLine($"Price: {book.Price}$");

            _outputEnvironment.WriteLine("Users which wish:");
            foreach (var clientId in book.WishedClientsId)
            {
                var client = _clientService.Get(clientId);
                _outputEnvironment.WriteLine($"\t{client}");
            }

            _outputEnvironment.WriteLine("Comments:");
            _commentService
                .GetAll()
                .Where(c => c.BookId == book.Id)
                .ToList()
                ?.ForEach(c => _outputEnvironment.WriteLine($"\t{c}"));

            var currentClient = _authService.GetCurrentClient();
            if (currentClient != null)
            {
                UserAction(currentClient, book);
            }

            _outputEnvironment.ReadKey();
        }

        public void UserAction(ClientDto currentClient, BookDto book)
        {
            _outputEnvironment.WriteLine("Your action:");

            var isWished = currentClient.WishedBooksId.Contains(book.Id);
            var isCommented = (from commentId in book.UserCommentsId
                               where currentClient.CommentsId.Contains(commentId)
                               select true).FirstOrDefault();

            var userMenu = _menuVisualizer.FactoryMethod();
            userMenu.Add(isWished ? "Remove from WishList" : "Add to WishList", () => AddRemoveWishlist(book, isWished))
                    .Add(isCommented ? "Remove comment" : "Add comment", () => AddRemoveComment(book, isCommented))
                    .Add("Return back", () => { })
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
                    _logger.LogError(ex, ex.Message);
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
                    _logger.LogError(ex, ex.Message);
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
                    _logger.LogError(ex, ex.Message);
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
                    _logger.LogError(ex, ex.Message);
                }
            }
        }

        public void AddBook()
        {
            var book = EnterBookData();

            try
            {
                _bookService.Create(book);
                _logger.LogInformation("Created success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
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
                _logger.LogInformation("Updated success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public void RemoveBook()
        {
            var books = _bookService.GetAll();

            _menuVisualizer.ShowCollection(books);

            _outputEnvironment.Write("Your choice: ");
            var choice = _outputEnvironment.ReadInt(1, books.Count);

            try
            {
                _bookService.Delete(books[choice - 1].Id);
                _logger.LogInformation("Deleted success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private BookDto EnterBookData()
        {
            _outputEnvironment.Write("Enter new name: ");
            var name = _outputEnvironment.Read();

            _outputEnvironment.Write("Enter new author: ");
            var author = _outputEnvironment.Read();

            _outputEnvironment.Write("Enter new genre: ");
            var genre = _outputEnvironment.Read();

            _outputEnvironment.Write("Enter new price: ");
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
