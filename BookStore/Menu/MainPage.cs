namespace BookStore.ConsoleApp.Menu
{
    using System;

    using BookStore.BLL.Interfaces;
    using BookStore.Shared.DTOs;

    public class MainPage
    {
        public MainPage(
            IService<BookDto> bookService,
            IService<ClientDto> clientService,
            IService<CommentDto> commentService,
            IService<WishDto> wishListService)
        {
            this.BookService = bookService;
            this.ClientService = clientService;
            this.CommentService = commentService;
            this.WishListService = wishListService;
        }

        public IService<BookDto> BookService { get; }

        public IService<ClientDto> ClientService { get; }

        public IService<CommentDto> CommentService { get; }

        public IService<WishDto> WishListService { get; }

        public ClientDto CurrentClient { get; set; }

        public void Run()
        {
            bool run = true;
            while (run)
            {
                var mainMenu = new MenuVisualizer();
                mainMenu.Add("Login/Logout", () => LoginLogout())
                    .Add("User menu", () => new ClientMenuPage(this).Run())
                    .Add("Book menu", () => new BookMenuPage(this).Run())
                    .Add("Exit", () => run = false);

                mainMenu.Display();

                Console.WriteLine();
                Console.WriteLine(new string('-', 30));
            }
        }

        private void LoginLogout()
        {
            if (CurrentClient != null)
            {
                CurrentClient = null;
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
                CurrentClient = ClientService.Find(client);
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
