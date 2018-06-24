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

        public void AddClient(string firstName, string secondName)
        {
            if (clients.Exists(client => (client.FirstName == firstName) && (client.SecondName == secondName)))
            {
                throw new ArgumentException($"Client: {firstName} {secondName} already exists");
            }

            clients.Add(new Client(firstName, secondName));
        }

        public void RemoveClient(string firstName, string secondName)
        {
            if (!clients.Exists(client => (client.FirstName == firstName) && (client.SecondName == secondName)))
            {
                throw new ArgumentException($"Client: {firstName} {secondName} doesn`t exist");
            }

            clients.RemoveAll(client => (client.FirstName == firstName) && (client.SecondName == secondName));
        }
    }
}