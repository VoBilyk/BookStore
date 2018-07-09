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
        }

        static void Initializate()
        {
            BookCatalog.Instance.AddBook(new Book("The Dark Half", "Stephen King", "Fantasy", 40));
            BookCatalog.Instance.AddBook(new Book("It", "Stephen King", "Horror", 66));
            BookCatalog.Instance.AddBook(new Book("Harry Potter", "Joanne Rowling", "Fantasy", 30));


            ClentService.Instance.AddClient(new Client("Volodymyr", "Bilyk"));
            ClentService.Instance.AddClient(new Client("Roman", "Velikiy"));
            ClentService.Instance.AddClient(new Client("Dmytro", "Horobriy"));
        }
    }
}
