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
            Initializate();

            Menu.Run();
            Console.ReadKey();
        }

        static void Initializate()
        {
            BookCatalog.Instance.AddBook(new Book("The Dark Half", "Stephen King", "Fantasy", 40));
            BookCatalog.Instance.AddBook(new Book("It", "Stephen King", "Horror", 66));
            BookCatalog.Instance.AddBook(new Book("Harry Potter", "Joanne Rowling", "Fantasy", 30));

            Clients.Instance.AddClient(new Client("Volodymyr", "Bilyk"));
            Clients.Instance.AddClient(new Client("Roman", "Velikiy"));
            Clients.Instance.AddClient(new Client("Dmytro", "Horobriy"));
        }
    }
}
