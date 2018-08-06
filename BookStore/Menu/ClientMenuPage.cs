namespace BookStore.ConsoleApp.Menu
{
    using System;
    using System.Linq;

    using BookStore.Shared.DTOs;

    public class ClientMenuPage
    {
        private MainPage _parentPage;

        public ClientMenuPage(MainPage parentPage)
        {
            this._parentPage = parentPage;
        }

        public void Run()
        {
            var menu = new MenuVisualizer()
                .Add("Show clients", () => ShowClients())
                .Add("Add client", () => AddClient())
                .Add("Update client", () => UpdateClient())
                .Add("Remove client", () => RemoveClient());
            menu.Display();
        }

        private void ShowClients()
        {
            var clients = _parentPage.ClientService.GetAll();

            var menu = new MenuVisualizer();
            menu.ShowCollection(clients);

            Console.Write("Choose someone: ");
            int choice = menu.ReadInt(1, clients.Count);

            ShowDetails(clients[choice - 1]);
        }

        private void ShowDetails(ClientDto client)
        {
            Console.WriteLine($"\nFirstname: {client.FirstName}");
            Console.WriteLine($"Lastname: {client.SecondName}");
            Console.WriteLine($"Email: {client.Email}");
            Console.WriteLine($"Address: {client.Address}");
            Console.WriteLine($"Birthdate: {client.BirthDate.ToLocalTime()}");

            Console.WriteLine("WishList:");
            foreach (var bookId in client.WishedBooksId)
            {
                var book = _parentPage.BookService.Get(bookId);
                Console.WriteLine($"\t{book}");
            }

            Console.WriteLine("Comments:");
            _parentPage.CommentService
                .GetAll()
                .Where(c => c.ClientId == client.Id)
                .ToList()
                ?.ForEach(c => Console.WriteLine($"\t{c}"));

            Console.ReadKey();
        }

        private void AddClient()
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter second name: ");
            string secondName = Console.ReadLine();

            var client = new ClientDto
            {
                FirstName = firstName,
                SecondName = secondName
            };

            try
            {
                _parentPage.ClientService.Create(client);
                Console.WriteLine("Success\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void UpdateClient()
        {
            var clients = _parentPage.ClientService.GetAll();

            var menu = new MenuVisualizer();
            menu.ShowCollection(clients);
            int choice = menu.ReadInt(1, clients.Count);

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter second name: ");
            string secondName = Console.ReadLine();

            var client = new ClientDto
            {
                FirstName = firstName,
                SecondName = secondName
            };

            try
            {
                _parentPage.ClientService.Update(clients[choice - 1].Id, client);
                Console.WriteLine("Success\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void RemoveClient()
        {
            var clients = _parentPage.ClientService.GetAll();

            var menu = new MenuVisualizer();
            menu.ShowCollection(clients);

            Console.Write("Your choice: ");
            int choice = menu.ReadInt(1, clients.Count);

            try
            {
                _parentPage.ClientService.Delete(clients[choice - 1].Id);
                Console.WriteLine("Success\n");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
