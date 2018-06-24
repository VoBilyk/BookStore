using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Model;

namespace BookStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var catalog = BookCatalog.Instance;

            catalog.AddBook(new Book("The Dark Half", "Stephen King", "Fantasy", 40));
            catalog.AddBook(new Book("It", "Stephen King", "Horror", 66));
            catalog.AddBook(new Book("Harry Potter", "Joanne Rowling", "Fantasy", 30));

            Console.WriteLine("--- Aftter adding books:");
            ShowBooks();

            Console.WriteLine("\n--- After updating book \"Harry Potter\"");
            var newBook = new Book("Harry Potter, part 7", "Joanne Rowling", "Fantasy", 129);
            var oldBook = catalog.GetBooks.First(book => book.Name == "Harry Potter");
            BookCatalog.Instance.UpdateBook(oldBook, newBook);
            ShowBooks();

            Console.WriteLine("\n--- After removing book \"It\"");
            catalog.RemoveBook(catalog.GetBooks.First(book => book.Name == "It"));
            ShowBooks();

            Console.ReadKey();
        }

        static void ShowBooks()
        {
            foreach (var book in BookCatalog.Instance.GetBooks)
            {
                Console.WriteLine(book);
            }
        }
    }
}
