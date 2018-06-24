using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    class Clients
    {
        private static readonly Lazy<Clients> lazy = new Lazy<Clients>(() => new Clients());
        public static Clients Instance { get { return lazy.Value; } }

        private List<Client> clients;

        public IEnumerable<Client> GetClients { get { return clients; } }

        private Clients()
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