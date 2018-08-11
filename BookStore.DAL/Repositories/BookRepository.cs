namespace BookStore.DAL.Repositories
{
    using BookStore.DAL.Interfaces.Repositories;
    using BookStore.DAL.Models;

    /// <inheritdoc/>
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="source">Data source</param>
        public BookRepository(DataSource source)
            : base(source.Books)
        {
        }
    }
}
