namespace BookStore.DAL
{
    using System;
    using System.Collections.Generic;
    using Bogus;

    using BookStore.DAL.Models;

    /// <summary>
    /// Container for data collection
    /// </summary>
    public class DataSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataSource"/> class.
        /// </summary>
        /// <param name="amount">Amount of generated items on each collection</param>
        public DataSource(int amount = 10)
        {
            Seed(amount);
        }

        /// <summary>
        /// Gets book collection
        /// </summary>
        public List<Book> Books { get; private set; }

        /// <summary>
        /// Gets client collection
        /// </summary>
        public List<Client> Clients { get; private set; }

        /// <summary>
        /// Gets comment collection
        /// </summary>
        public List<Comment> Comments { get; private set; }

        /// <summary>
        /// Gets wish list collection
        /// </summary>
        public List<Wish> WishList { get; private set; }

        /// <summary>
        /// Added random data for data source
        /// </summary>
        /// <param name="amount">Amount of generated elements in each table</param>
        private void Seed(int amount)
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
                .RuleFor(o => o.BirthDate, f => f.Date.Past(50))
                .RuleFor(o => o.Email, f => f.Internet.Email())
                .RuleFor(o => o.Address, f => f.Address.FullAddress());

            var commentFaker = new Faker<Comment>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.Text, f => f.Lorem.Sentence())
                .RuleFor(o => o.Book, f => f.PickRandom(Books))
                .RuleFor(o => o.Client, f => f.PickRandom(Clients));

            var wishListFaker = new Faker<Wish>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.BookId, f => f.PickRandom(Books).Id)
                .RuleFor(o => o.ClientId, f => f.PickRandom(Clients).Id);

            // Generating date
            Books = bookFaker.Generate(amount);
            Clients = clientFaker.Generate(amount);
            Comments = commentFaker.Generate(amount);
            WishList = wishListFaker.Generate(amount);
        }
    }
}
