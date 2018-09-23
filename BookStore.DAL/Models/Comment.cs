namespace BookStore.DAL.Models
{
    using System;

    using BookStore.DAL.Interfaces;

    /// <summary>
    /// Comment entity
    /// </summary>
    public class Comment : IEntity
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Client Client { get; set; }

        public Book Book { get; set; }
    }
}
