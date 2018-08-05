namespace BookStore.ConsoleApp.Menu
{
    using System;

    using BookStore.BLL.Interfaces;
    using BookStore.Shared.DTOs;

    public class ClientMenuPage
    {
        private IService<ClientDto> _clientService;

        public ClientMenuPage(IService<ClientDto> clientService)
        {
            this._clientService = clientService;
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
            var clients = _clientService.GetAll();

            var menu = new MenuVisualizer();
            menu.ShowCollection(clients);
            Console.WriteLine();
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
                _clientService.Create(client);
                Console.WriteLine("Success\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void UpdateClient()
        {
            var clients = _clientService.GetAll();

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
                _clientService.Update(clients[choice - 1].Id, client);
                Console.WriteLine("Success\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void RemoveClient()
        {
            var clients = _clientService.GetAll();

            var menu = new MenuVisualizer();
            menu.ShowCollection(clients);

            Console.Write("Your choice: ");
            int choice = menu.ReadInt(1, clients.Count);

            try
            {
                _clientService.Delete(clients[choice - 1].Id);
                Console.WriteLine("Success\n");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
