namespace BookStore.DAL.Models
{
    using System;

    using BookStore.DAL.Interfaces;

    /// <summary>
    /// Wish entity
    /// </summary>
    public class Wish : IEntity
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid BookId { get; set; }
    }
}
