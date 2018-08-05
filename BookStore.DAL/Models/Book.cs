namespace BookStore.DAL.Models
{
    using System;

    using BookStore.DAL.Interfaces;

    public class Book : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }
    }
}
