namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using Microsoft.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.Shared.DTOs;

    /// <summary>
    /// Page form working with client
    /// </summary>
    public class ClientMenuPage
    {
        private readonly ILogger<BookMenuPage> _logger;
        private readonly IClientService _clientService;
        private readonly IBookService _bookService;
        private readonly ICommentService _commentService;

        public ClientMenuPage(
            ILogger<BookMenuPage> logger,
            IClientService clientService,
            IBookService bookService,
            ICommentService commentService)
        {
            this._logger = logger;
            this._clientService = clientService;
            this._bookService = bookService;
            this._commentService = commentService;
        }

        public void Run()
        {
            var menu = new MenuVisualizer()
                .Add("Show clients", () => ShowClients())
                .Add("Add client", () => AddClient())
                .Add("Update client", () => UpdateClient())
                .Add("Remove client", () => RemoveClient())
                .Add("Return back", () => { return; });
            menu.Display();
        }

        private void ShowClients()
        {
            var clients = _clientService.GetAll();

            MenuVisualizer.ShowCollection(clients);

            Console.Write("Choose someone: ");
            int choice = MenuVisualizer.ReadInt(1, clients.Count);

            ShowDetails(clients[choice - 1]);
        }

        private void ShowDetails(ClientDto client)
        {
            Console.WriteLine($"\nFirstname: {client.FirstName}");
            Console.WriteLine($"Lastname: {client.LastName}");
            Console.WriteLine($"Email: {client.Email}");
            Console.WriteLine($"Address: {client.Address}");
            Console.WriteLine($"Birthdate: {client.BirthDate.ToLocalTime()}");

            Console.WriteLine("WishList:");
            foreach (var bookId in client.WishedBooksId)
            {
                var book = _bookService.Get(bookId);
                Console.WriteLine($"\t{book}");
            }

            Console.WriteLine("Comments:");
            foreach (var commentId in client.CommentsId)
            {
                var comment = _commentService.Get(commentId);
                Console.WriteLine($"\t{comment}");
            }

            Console.ReadKey();
        }

        private void AddClient()
        {
            var client = EnterClientData();

            try
            {
                _clientService.Create(client);
                _logger.LogInformation("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void UpdateClient()
        {
            var clients = _clientService.GetAll();
            MenuVisualizer.ShowCollection(clients);

            Console.Write("Your choice: ");
            int choice = MenuVisualizer.ReadInt(1, clients.Count);

            var client = EnterClientData();

            try
            {
                _clientService.Update(clients[choice - 1].Id, client);
                _logger.LogInformation("Updated success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private void RemoveClient()
        {
            var clients = _clientService.GetAll();

            MenuVisualizer.ShowCollection(clients);

            Console.Write("Your choice: ");
            int choice = MenuVisualizer.ReadInt(1, clients.Count);

            try
            {
                _clientService.Delete(clients[choice - 1].Id);
                _logger.LogInformation("Deleted success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private ClientDto EnterClientData()
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            string secondName = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            return new ClientDto
            {
                FirstName = firstName,
                LastName = secondName,
                Email = email
            };
        }
    }
}
