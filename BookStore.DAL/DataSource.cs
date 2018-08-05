namespace BookStore.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;

    using BookStore.DAL.Models;

    public class DataSource
    {
        public DataSource(int amount = 10)
        {
            string[] bookGenres = { "Drama", "Mystery", "Horror", "Romance", "Science", "History", "Fantasy" };

            var bookFaker = new Faker<Book>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.Name, f => f.Lorem.Word())
                .RuleFor(o => o.Author, f => f.Name.FullName())
                .RuleFor(o => o.Genre, f => f.PickRandom(bookGenres))
                .RuleFor(o => o.Price, f => Math.Round(f.Random.Decimal(5, 100), 2));

            var clientFaker = new Faker<Client>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.SecondName, f => f.Name.LastName())
                .RuleFor(o => o.WishList, f => f.PickRandom(Books, Randomizer.Seed.Next(0, 3)).ToList());

            var commentFaker = new Faker<Comment>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.Book, f => f.PickRandom(Books))
                .RuleFor(o => o.Client, f => f.PickRandom(Clients))
                .RuleFor(o => o.Text, f => f.Lorem.Sentence());

            // Generating date
            Books = bookFaker.Generate(amount);
            Clients = clientFaker.Generate(amount);
            Comments = commentFaker.Generate(amount);
        }

        public List<Book> Books { get; }

        public List<Client> Clients { get; }

        public List<Comment> Comments { get; }
    }
}
