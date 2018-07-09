using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    class ClentService
    {
        private static readonly Lazy<ClentService> lazy = new Lazy<ClentService>(() => new ClentService());
        public static ClentService Instance { get { return lazy.Value; } }

        private List<Client> clients;

        public IEnumerable<Client> GetClients { get { return clients; } }

        private ClentService()
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