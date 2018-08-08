namespace BookStore.DAL.Models
{
    using System;

    using BookStore.DAL.Interfaces;

    /// <summary>
    /// Client entity
    /// </summary>
    public class Client : IEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}
