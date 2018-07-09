using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    class Book
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Author { get; private set; }

        public string Genre { get; private set; }

        public decimal Price { get; private set; }
  

        public Book(string name, string author, string genre, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Author = author;
            Genre = genre;
            Price = price;
        }

        public void Update(string name, string author, string genre, decimal price)
        {
            Name = name;
            Author = author;
            Genre = genre;
            Price = price;
        }

        public override string ToString()
        {
            return $"Book id: {Id}, name: {Name}, author: {Author}, genre: {Genre}, price: {Price}";
        }
    }
}
