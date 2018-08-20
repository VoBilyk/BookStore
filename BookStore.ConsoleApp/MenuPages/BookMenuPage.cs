namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp;
    using BookStore.Shared.DTOs;

    /// <summary>
    /// Page for working with books
    /// </summary>
    public class BookMenuPage
    {
        private readonly ILogger<BookMenuPage> _logger;
        private readonly IClientService _clientService;
        private readonly IAuthService _authService;
        private readonly IBookService _bookService;
        private readonly ICommentService _commentService;
        private readonly IWishListService _wishListService;

        public BookMenuPage(
            ILogger<BookMenuPage> logger,
            IClientService clientService,
            IAuthService authService,
            IBookService bookService,
            ICommentService commentService,
            IWishListService wishListService)
        {
            this._logger = logger;
            this._clientService = clientService;
            this._authService = authService;
            this._bookService = bookService;
            this._commentService = commentService;
            this._wishListService = wishListService;
        }

        public void Run()
        {
            var menu = new MenuVisualizer()
                .Add("Show books", () => ShowBooks())
                .Add("Add book", () => AddBook())
                .Add("Update book", () => UpdateBook())
                .Add("Remove book", () => RemoveBook())
                .Add("Return back", () => { });
            menu.Display();
        }

        private void ShowBooks()
        {
            var books = _bookService.GetAll();

            MenuVisualizer.ShowCollection(books);
            Console.Write("\nChoose someone: ");
            var choice = MenuVisualizer.ReadInt(1, books.Count);

            ShowDetails(books[choice - 1]);
        }

        private void ShowDetails(BookDto book)
        {
            Console.WriteLine($"\nName: {book.Name}");
            Console.WriteLine($"Genre: {book.Genre}");
            Console.WriteLine($"Author: {book.Author}");
            Console.WriteLine($"Price: {book.Price}$");

            Console.WriteLine("Users which wish:");
            foreach (var clientId in book.WishedClientsId)
            {
                var client = _clientService.Get(clientId);
                Console.WriteLine($"\t{client}");
            }

            Console.WriteLine("Comments:");
            _commentService
                .GetAll()
                .Where(c => c.BookId == book.Id)
                .ToList()
                ?.ForEach(c => Console.WriteLine($"\t{c}"));

            var currentClient = _authService.GetCurrentClient();
            if (currentClient != null)
            {
                UserAction(currentClient, book);
            }

            Console.ReadKey();
        }

        private void UserAction(ClientDto currentClient, BookDto book)
        {
            Console.WriteLine("Your action:");

            var isWished = currentClient.WishedBooksId.Contains(book.Id);
            var isCommented = (from commentId in book.UserCommentsId
                               where currentClient.CommentsId.Contains(commentId)
                               select true).FirstOrDefault();

            var userMenu = new MenuVisualizer();
            userMenu.Add(isWished ? "Remove from WishList" : "Add to WishList", () => AddRemoveWishlist(book, isWished))
                    .Add(isCommented ? "Remove comment" : "Add comment", () => AddRemoveComment(book, isCommented))
                    .Add("Return back", () => { })
                    .Display();
        }

        private void AddRemoveWishlist(BookDto book, bool remove)
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

        private void AddRemoveComment(BookDto book, bool remove)
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
                        Text = Console.ReadLine()
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
        }

        private void AddBook()
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

        private void UpdateBook()
        {
            var books = _bookService.GetAll();
            MenuVisualizer.ShowCollection(books);
            int choice = MenuVisualizer.ReadInt(1, books.Count);

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

        private void RemoveBook()
        {
            var books = _bookService.GetAll();

            MenuVisualizer.ShowCollection(books);

            Console.Write("Your choice: ");
            int choice = MenuVisualizer.ReadInt(1, books.Count);

            try
            {
                _bookService.Delete(books[choice - 1].Id);
                _logger.LogInformation("Deleted success");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private BookDto EnterBookData()
        {
            Console.Write("Enter new name: ");
            string name = Console.ReadLine();

            Console.Write("Enter new author: ");
            string author = Console.ReadLine();

            Console.Write("Enter new genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter new price: ");
            string strPrice = Console.ReadLine();

            decimal.TryParse(strPrice, out decimal price);

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
