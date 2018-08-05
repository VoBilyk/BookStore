namespace BookStore.ConsoleApp.Menu
{
    using System;

    using BookStore.BLL.Interfaces;
    using BookStore.Shared.DTOs;

    public class MainPage
    {
        private readonly IService<BookDto> _bookService;
        private readonly IService<ClientDto> _clientService;
        private readonly IService<CommentDto> _commentService;

        private ClientDto currentClient;

        public MainPage(IService<BookDto> bookService, IService<ClientDto> clientService, IService<CommentDto> commentService)
        {
            this._bookService = bookService;
            this._clientService = clientService;
            this._commentService = commentService;
        }

        public void Run()
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine();
                Console.WriteLine(new string('-', 30));

                var mainMenu = new MenuVisualizer();
                mainMenu.Add("Login/Logout", () => LoginLogout())
                    .Add("Show information about me", () => Console.WriteLine("Not implemented yet"))
                    .Add("User menu", () => new ClientMenuPage(_clientService).Run())
                    .Add("Book menu", () => new BookMenuPage(_bookService).Run())
                    .Add("Exit", () => run = false);

                mainMenu.Display();
            }
        }

        private void LoginLogout()
        {
            if (currentClient != null)
            {
                currentClient = null;
                Console.WriteLine("Logout Success");
                return;
            }

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
                currentClient = _clientService.Find(client);
                Console.WriteLine("Login Success");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
