using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Services;

namespace BookStore
{
    static class Menu
    {
        static private bool run = true;
        static private Client currenClient;

        public static void Run()
        {
            while (run)
            {
                Console.Clear();

                ShowCommands();
                ChooseCommand();
            }
        }

        static private void ShowCommands()
        {
            Console.WriteLine("---BookStore menu---");
            Console.WriteLine("1. Login/Logout");
            Console.WriteLine("2. Show books list");
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("3. Add book");
            Console.WriteLine("4. Update book");
            Console.WriteLine("5. Remove book");
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("6. Show clients list");
            Console.WriteLine("7. Add client");
            Console.WriteLine("8. Remove client");
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("9. Exit\n");
        }


        static public void ChooseCommand()
        {
            int choice = 0;

            Console.Write("Enter your choice: ");
            Int32.TryParse(Console.ReadLine(), out choice);

            Console.WriteLine(new string('-', 35));

            switch (choice)
            {
                case 1:
                    LoginLogout();
                    break;

                case 2:
                    ShowBooks();

                    if (currenClient != null)
                    {
                        ClientActionWithBook();
                    }

                    break;

                case 3:
                    AddBook();
                    break;

                case 4:
                    ShowBooks();
                    UpdateBook();
                    break;

                case 5:
                    ShowBooks();
                    RemoveBook();
                    break;

                case 6:
                    ShowClients();
                    break;

                case 7:
                    AddClient();
                    break;

                case 8:
                    RemoveClient();
                    break;

                case 9:
                    run = false;
                    break;

                default:
                    Console.WriteLine("Error. Wrong command");
                    break;
            }

            Console.WriteLine(new string('-', 35));
            Console.WriteLine("\n\n");

            Console.ReadKey();
        }

        static private void ShowBooks()
        {
            var books = BookCatalog.Instance.GetBooks;

            int counter = 0;
            foreach (var book in BookCatalog.Instance.GetBooks)
            {
                Console.WriteLine($"{counter++}. {book}");
            }
        }

        static private Book ChooseBookByNumber()
        {
            int choice = 0;

            Console.Write("Choose book: ");
            Int32.TryParse(Console.ReadLine(), out choice);
            Console.WriteLine(new string('-', 20));

            try
            {
                return BookCatalog.Instance.GetBooks.ElementAt(choice);
            }
            catch (Exception)
            {
                Console.WriteLine("Uncorrect choice");
                return null;
            }
        }

        static private void AddBook()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter author: ");
            string author = Console.ReadLine();

            Console.Write("Enter genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter price: ");
            string strPrice = Console.ReadLine();

            decimal price;
            Decimal.TryParse(strPrice, out price);

            try
            {
                BookCatalog.Instance.AddBook(new Book(name, author, genre, price));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static private void UpdateBook()
        {
            var oldBook = ChooseBookByNumber();

            if (oldBook == null)
            {
                return;
            }

            Console.Write("Enter new name: ");
            string name = Console.ReadLine();

            Console.Write("Enter new author:");
            string author = Console.ReadLine();

            Console.Write("Enter new genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter new price: ");
            string strPrice = Console.ReadLine();

            decimal price;
            Decimal.TryParse(strPrice, out price);

            try
            {
                BookCatalog.Instance.UpdateBook(oldBook, new Book(name, author, genre, price));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static private void RemoveBook()
        {
            var foundBook = ChooseBookByNumber();

            if (foundBook == null)
            {
                return;
            }

            try
            {
                BookCatalog.Instance.RemoveBook(foundBook);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }


        static private void ShowClients()
        {
            var books = BookCatalog.Instance.GetBooks;

            foreach (var client in ClientService.Instance.GetClients)
            {
                Console.WriteLine(client);

                Console.WriteLine("--- Comments ---");
                foreach (var comment in CommentService.Instance.FindByClient(client))
                {
                    Console.WriteLine($"{comment.Book.Name}: {comment.Text}");
                }

                Console.WriteLine("--- Whish list ---");
                foreach (var wish in WishList.Instance.FindByClient(client))
                {
                    Console.WriteLine(wish.Book.Name);
                }

                Console.WriteLine(new string('-', 20));
            }
        }

        static private void AddClient()
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter second name: ");
            string secondName = Console.ReadLine();

            try
            {
                ClientService.Instance.AddClient(new Client(firstName, secondName));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static private void RemoveClient()
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter second name: ");
            string secondName = Console.ReadLine();

            var foundClient = ClientService.Instance.GetClients?.FirstOrDefault(client =>
                (client.FirstName == firstName) && (client.SecondName == secondName));

            try
            {
                ClientService.Instance.RemoveClient(foundClient);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static private void LoginLogout()
        {
            if (currenClient != null)
            {
                currenClient = null;
                Console.WriteLine("Logout Success");
                return;
            }

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter second name: ");
            string secondName = Console.ReadLine();

            var foundClient = ClientService.Instance.GetClients?.FirstOrDefault(client =>
                (client.FirstName == firstName) && (client.SecondName == secondName));

            if (foundClient != null)
            {
                currenClient = foundClient;
                Console.WriteLine("Login Success");
                return;
            }

            Console.WriteLine("Client don`t exists");
        }

        static private void ClientActionWithBook()
        {
            var currentBook = ChooseBookByNumber();

            if (currentBook == null) { return; }

            Console.WriteLine(currentBook);
            Console.WriteLine("1. Add to WishList");
            Console.WriteLine("2. Leave comment");

            int choice;
            Int32.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    {
                        try
                        {
                            WishList.Instance.AddWish(new Wish(currenClient, currentBook));
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }

                case 2:
                    {
                        var text = Console.ReadLine();

                        try
                        {
                            CommentService.Instance.AddComment(new Comment(currenClient, currentBook, text));
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    }
            }
        }
    }
}
