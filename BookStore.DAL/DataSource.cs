using System;
using System.Collections.Generic;
using Bogus;
using BookStore.DAL.Models;


namespace BookStore.DAL
{
    public class DataSource
    {
        static List<Book> books;

        static List<Client> clients;

        static List<Comment> comments;

        static DataSource()
        {
            string[] bookGenres = { "Drama", "Mystery", "Horror", "Romance", "Science", "History", "Fantasy" };

            var bookFaker = new Faker<Book>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.Name, f => f.Lorem.Word())
                .RuleFor(o => o.Name, f => f.Name.FullName())
                .RuleFor(o => o.Genre, f => f.PickRandom(bookGenres))
                .RuleFor(o => o.Price, f => f.Random.Decimal(5, 100));

            var clientFaker = new Faker<Client>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.SecondName, f => f.Name.LastName())
                .RuleFor(o => o.WishList, f => f.PickRandom(books, Randomizer.Seed.Next(0, 3)));

            var commentFaker = new Faker<Comment>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.Book, f => f.PickRandom(books))
                .RuleFor(o => o.Client, f => f.PickRandom(clients))
                .RuleFor(o => o.Text, f => f.Lorem.Sentence());

            // Generating date
            bookFaker.Generate(10);
            clientFaker.Generate(10);
            commentFaker.Generate(10);
        }

        public List<Book> Books => books;

        public List<Client> Clients => clients;

        public List<Comment> Comments => comments;
    }
}
