using System;
using System.Collections.Generic;
using BookStore.DAL.Interfaces;

namespace BookStore.DAL.Models
{
    public class Client : IEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public List<Book> WishList { get; set; }
    }
}
