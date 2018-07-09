using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    class Comment
    {
        public Client Client { get; private set; }

        public Book Book { get; private set; }

        public string Text { get; private set; }

        public Comment(Client client, Book book, string text)
        {
            Client = client;
            Book = book;
            Text = text;
        }

        public override string ToString()
        {
            return $"Comment by: {Client}, for: {Book}.\nText: {Text}";
        }
    }
}
