using System;
using System.Collections.Generic;
using BookStore.Models;

namespace BookStore.Services
{
    class ClientService
    {
        private static readonly Lazy<ClientService> lazy = new Lazy<ClientService>(() => new ClientService());
        public static ClientService Instance { get { return lazy.Value; } }

        private List<Client> clients;

        public IEnumerable<Client> GetClients { get { return clients; } }

        private ClientService()
        {
            clients = new List<Client>();
        }

        public void AddClient(Client client)
        {
            if (clients.Contains(client))
            {
                throw new ArgumentException($"{client} already exists");
            }

            clients.Add(client);
        }

        public void RemoveClient(Client client)
        {
            if (!clients.Contains(client))
            {
                throw new ArgumentException($"{client} doesn`t exist");
            }

            clients.Remove(client);
        }
    }
}