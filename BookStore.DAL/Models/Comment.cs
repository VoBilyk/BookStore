using System;
using BookStore.DAL.Interfaces;

namespace BookStore.DAL.Models
{
    public class Comment : IEntity
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Client Client { get; set; }

        public Book Book { get; set; }


        public override string ToString()
        {
            return $"{Text}";
        }
    }
}
