namespace BookStore.ConsoleApp.Menu
{
    using System;

    using BookStore.BLL.Interfaces;
    using BookStore.Shared.DTOs;

    public class BookMenuPage
    {
        private IService<BookDto> _bookService;

        public BookMenuPage(IService<BookDto> bookService)
        {
            this._bookService = bookService;
        }

        public void Run()
        {
            var menu = new MenuVisualizer()
                .Add("Show books", () => this.ShowBooks())
                .Add("Add book", () => this.AddBook())
                .Add("Update book", () => this.AddBook())
                .Add("Remove book", () => this.RemoveBook());
            menu.Display();
        }

        private void ShowBooks()
        {
            var books = _bookService.GetAll();

            var menu = new MenuVisualizer();
            menu.ShowCollection(books);
            Console.WriteLine();
            Console.ReadKey();
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
                _bookService.Create(book);
                Console.WriteLine("Success\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void UpdateBook()
        {
            var books = _bookService.GetAll();

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
                _bookService.Update(books[choice - 1].Id, book);
                Console.WriteLine("Success\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void RemoveBook()
        {
            var books = _bookService.GetAll();

            var menu = new MenuVisualizer();
            menu.ShowCollection(books);

            Console.Write("Your choice: ");
            int choice = menu.ReadInt(1, books.Count);

            try
            {
                _bookService.Delete(books[choice - 1].Id);
                Console.WriteLine("Success\n");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
