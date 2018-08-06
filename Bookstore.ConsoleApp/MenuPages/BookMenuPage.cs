namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using System.Linq;

    using BookStore.Shared.DTOs;

    public class BookMenuPage
    {
        private MainPage _parentPage;

        public BookMenuPage(MainPage parentPage)
        {
            _parentPage = parentPage;
        }

        public void Run()
        {
            var menu = new MenuVisualizer()
                .Add("Show books", () => ShowBooks())
                .Add("Add book", () => AddBook())
                .Add("Update book", () => AddBook())
                .Add("Remove book", () => RemoveBook());
            menu.Display();
        }

        private void ShowBooks()
        {
            var books = _parentPage.BookService.GetAll();

            var menu = new MenuVisualizer();
            menu.ShowCollection(books);
            Console.Write("\nChoose someone: ");
            int choice = menu.ReadInt(1, books.Count);

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
                var client = _parentPage.ClientService.Get(clientId);
                Console.WriteLine($"\t{client}");
            }

            Console.WriteLine("Comments:");
            _parentPage.CommentService
                .GetAll()
                .Where(c => c.BookId == book.Id)
                .ToList()
                ?.ForEach(c => Console.WriteLine($"\t{c}"));

            if (_parentPage.CurrentClient != null)
            {
                UserAction(book);
            }

            Console.ReadKey();
        }

        private void UserAction(BookDto book)
        {
            Console.WriteLine("Your action:");

            var isWished = _parentPage.CurrentClient.WishedBooksId.Contains(book.Id);
            var isCommented = (from commentId in book.UserCommentsId
                               where _parentPage.CurrentClient.CommentsId.Contains(commentId)
                               select true).FirstOrDefault();

            var userMenu = new MenuVisualizer();
            userMenu.Add(isWished ? "Remove from WishList" : "Add to WishList", () => AddRemoveWishlist(book, isWished))
                    .Add(isCommented ? "Remove comment" : "Add comment", () => AddRemoveComment(book, isCommented));

            userMenu.Display();
        }

        private void AddRemoveWishlist(BookDto book, bool remove)
        {
            if (remove == true)
            {
                try
                {
                    var wish = _parentPage.WishListService.GetAll()
                        .FirstOrDefault(c => c.BookId == book.Id && c.ClientId == _parentPage.CurrentClient.Id);

                    _parentPage.WishListService.Delete(wish.Id);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error. Can`t delete");
                }
            }
            else
            {
                try
                {
                    _parentPage.WishListService.Create(
                    new WishDto
                    {
                        ClientId = _parentPage.CurrentClient.Id,
                        BookId = book.Id
                    });
                }
                catch (Exception)
                {
                    Console.WriteLine("Error. Can`t create");
                }
            }

            _parentPage.CurrentClient = _parentPage.ClientService.Get(_parentPage.CurrentClient.Id);
        }

        private void AddRemoveComment(BookDto book, bool remove)
        {
            if (remove == true)
            {
                try
                {
                    var comment = _parentPage.CommentService.GetAll()
                        .FirstOrDefault(c => c.BookId == book.Id && c.ClientId == _parentPage.CurrentClient.Id);

                    _parentPage.CommentService.Delete(comment.Id);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error. Can`t delete");
                }
            }
            else
            {
                try
                {
                    _parentPage.CommentService.Create(
                    new CommentDto
                    {
                        ClientId = _parentPage.CurrentClient.Id,
                        BookId = book.Id,
                        Text = Console.ReadLine()
                    });
                }
                catch (Exception)
                {
                    Console.WriteLine("Error. Can`t create");
                }
            }

            _parentPage.CurrentClient = _parentPage.ClientService.Get(_parentPage.CurrentClient.Id);
        }

        private void AddBook()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter author: ");
            string author = Console.ReadLine();

            Console.Write("Enter genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter price: ");
            string strPrice = Console.ReadLine();

            decimal.TryParse(strPrice, out decimal price);

            var book = new BookDto
            {
                Name = name,
                Author = author,
                Genre = genre,
                Price = price
            };

            try
            {
                _parentPage.BookService.Create(book);
                Console.WriteLine("Success\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void UpdateBook()
        {
            var books = _parentPage.BookService.GetAll();

            var menu = new MenuVisualizer();
            menu.ShowCollection(books);
            int choice = menu.ReadInt(1, books.Count);

            Console.Write("Enter new name: ");
            string name = Console.ReadLine();

            Console.Write("Enter new author: ");
            string author = Console.ReadLine();

            Console.Write("Enter new genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter new price: ");
            string strPrice = Console.ReadLine();

            decimal.TryParse(strPrice, out decimal price);

            var book = new BookDto
            {
                Name = name,
                Author = author,
                Genre = genre,
                Price = price
            };

            try
            {
                _parentPage.BookService.Update(books[choice - 1].Id, book);
                Console.WriteLine("Success\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void RemoveBook()
        {
            var books = _parentPage.BookService.GetAll();

            var menu = new MenuVisualizer();
            menu.ShowCollection(books);

            Console.Write("Your choice: ");
            int choice = menu.ReadInt(1, books.Count);

            try
            {
                _parentPage.BookService.Delete(books[choice - 1].Id);
                Console.WriteLine("Success\n");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
