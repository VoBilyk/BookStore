namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using Microsoft.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.Shared.DTOs;

    /// <summary>
    /// Page form working with client
    /// </summary>
    public class ClientMenuPage : IPage
    {
        private readonly ILogger<ClientMenuPage> _logger;
        private readonly IOutputEnvironment _outputEnvironment;
        private readonly IMenuVisualizer _menuVisualizer;

        private readonly IClientService _clientService;
        private readonly IBookService _bookService;
        private readonly ICommentService _commentService;

        public ClientMenuPage(
            ILogger<ClientMenuPage> logger,
            IMenuVisualizer menuVisualizer,
            IOutputEnvironment outputEnvironment,
            IClientService clientService,
            IBookService bookService,
            ICommentService commentService)
        {
            this._outputEnvironment = outputEnvironment;
            this._logger = logger;
            this._menuVisualizer = menuVisualizer;

            this._clientService = clientService;
            this._bookService = bookService;
            this._commentService = commentService;
        }

        /// <inheritdoc/>
        public void Display()
        {
            var menu = _menuVisualizer.FactoryMethod()
                .Add("Show clients", () => ShowClients())
                .Add("Add client", () => AddClient())
                .Add("Update client", () => UpdateClient())
                .Add("Remove client", () => RemoveClient())
                .Add("Return back", () => { });
            menu.Display();
        }

        public void ShowClients()
        {
            var clients = _clientService.GetAll();

            _menuVisualizer.ShowCollection(clients);

            _outputEnvironment.Write("Choose someone: ");
            var choice = _outputEnvironment.ReadInt(1, clients.Count);

            ShowDetails(clients[choice - 1]);
        }

        public void ShowDetails(ClientDto client)
        {
            _outputEnvironment.WriteLine($"\nFirstname: {client.FirstName}");
            _outputEnvironment.WriteLine($"Lastname: {client.LastName}");
            _outputEnvironment.WriteLine($"Email: {client.Email}");
            _outputEnvironment.WriteLine($"Address: {client.Address}");
            _outputEnvironment.WriteLine($"Birthdate: {client.BirthDate.ToLocalTime()}");

            _outputEnvironment.WriteLine("WishList:");
            foreach (var bookId in client.WishedBooksId)
            {
                var book = _bookService.Get(bookId);
                _outputEnvironment.WriteLine($"\t{book}");
            }

            _outputEnvironment.WriteLine("Comments:");
            foreach (var commentId in client.CommentsId)
            {
                var comment = _commentService.Get(commentId);
                _outputEnvironment.WriteLine($"\t{comment}");
            }

            _outputEnvironment.ReadKey();
        }

        public void AddClient()
        {
            var client = EnterClientData();

            try
            {
                _clientService.Create(client);
                _logger.LogInformation("Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public void UpdateClient()
        {
            var clients = _clientService.GetAll();
            _menuVisualizer.ShowCollection(clients);

            _outputEnvironment.Write("Your choice: ");
            var choice = _outputEnvironment.ReadInt(1, clients.Count);

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

        public void RemoveClient()
        {
            var clients = _clientService.GetAll();

            _menuVisualizer.ShowCollection(clients);

            _outputEnvironment.Write("Your choice: ");
            var choice = _outputEnvironment.ReadInt(1, clients.Count);

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
            _outputEnvironment.Write("Enter first name: ");
            var firstName = _outputEnvironment.Read();

            _outputEnvironment.Write("Enter last name: ");
            var secondName = _outputEnvironment.Read();

            _outputEnvironment.Write("Enter email: ");
            var email = _outputEnvironment.Read();

            return new ClientDto
            {
                FirstName = firstName,
                LastName = secondName,
                Email = email
            };
        }
    }
}
