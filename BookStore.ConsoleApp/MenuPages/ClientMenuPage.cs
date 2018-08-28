namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.ConsoleApp.Interfaces;
    using BookStore.Shared.DTOs;
    using BookStore.Shared.Resources;

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
                .Add(Resource.ShowClients, () => ShowClients())
                .Add(Resource.Search, () => Find())
                .Add(Resource.AddClient, () => AddClient())
                .Add(Resource.UpdateClient, () => UpdateClient())
                .Add(Resource.RemoveClient, () => RemoveClient())
                .Add(Resource.ReturnBack, () => { });
            menu.Display();
        }

        public void ShowClients()
        {
            var clients = _clientService.GetAll();

            _menuVisualizer.ShowCollection(clients);

            _outputEnvironment.Write($"{Resource.ChooseSomeone}: ");
            var choice = _outputEnvironment.ReadInt(1, clients.Count);

            ShowDetails(clients[choice - 1]);
        }

        public void Find()
        {
            _outputEnvironment.Write($"{Resource.Search}: ");
            var query = _outputEnvironment.Read();

            var clients = _clientService.Find(query);

            if (!clients.Any())
            {
                _outputEnvironment.WriteLine(Resource.NotFound);
                return;
            }

            _menuVisualizer.ShowCollection(clients);

            _outputEnvironment.Write($"{Resource.ChooseSomeone}: ");
            var choice = _outputEnvironment.ReadInt(1, clients.Count);

            ShowDetails(clients[choice - 1]);
        }

        public void ShowDetails(ClientDto client)
        {
            _outputEnvironment.WriteLine($"\n{Resource.FirstName}: {client.FirstName}");
            _outputEnvironment.WriteLine($"{Resource.LastName}: {client.LastName}");
            _outputEnvironment.WriteLine($"{Resource.Email}: {client.Email}");
            _outputEnvironment.WriteLine($"{Resource.Address}: {client.Address}");
            _outputEnvironment.WriteLine($"{Resource.BirthDate}: {client.BirthDate.ToShortDateString()}");

            _outputEnvironment.WriteLine($"{Resource.WishList}:");

            if (!client.WishedBooksId.Any())
            {
                _outputEnvironment.WriteLine($"\t{Resource.NotHave}");
            }

            foreach (var bookId in client.WishedBooksId)
            {
                var book = _bookService.Get(bookId);
                _outputEnvironment.WriteLine($"\t{book}");
            }

            _outputEnvironment.WriteLine($"{Resource.Comments}:");

            if (!client.CommentsId.Any())
            {
                _outputEnvironment.WriteLine($"\t{Resource.NotHave}");
            }

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
                _logger.LogInformation(Resource.CreatedSuccess);
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

            _outputEnvironment.Write($"{Resource.YourChoice}: ");
            var choice = _outputEnvironment.ReadInt(1, clients.Count);

            var client = EnterClientData();

            try
            {
                _clientService.Update(clients[choice - 1].Id, client);
                _logger.LogInformation(Resource.UpdatedSuccess);
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

            _outputEnvironment.Write($"{Resource.YourChoice}: ");
            var choice = _outputEnvironment.ReadInt(1, clients.Count);

            try
            {
                _clientService.Delete(clients[choice - 1].Id);
                _logger.LogInformation(Resource.DeletedSuccess);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private ClientDto EnterClientData()
        {
            _outputEnvironment.Write($"{Resource.EnterFirstName}: ");
            var firstName = _outputEnvironment.Read();

            _outputEnvironment.Write($"{Resource.EnterLastName}: ");
            var lastName = _outputEnvironment.Read();

            _outputEnvironment.Write($"{Resource.EnterEmail}: ");
            var email = _outputEnvironment.Read();

            return new ClientDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }
    }
}
