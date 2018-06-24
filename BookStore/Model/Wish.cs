using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    class Wish
    {
        public Client Client { get; private set; }

        public Book Book { get; private set; }

        public Wish(Client client, Book book)
        {
            Client = client;
            Book = book;
        }
    }
}
