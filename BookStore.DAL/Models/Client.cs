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

        public ICollection<Book> WishList { get; set; }


        public override string ToString()
        {
            return $"Fullname: {FirstName} {SecondName}";
        }
    }
}
