namespace BookStore.DAL.Repositories
{
    using BookStore.DAL.Models;

    public class BookRepository : GenericRepository<Book>
    {
        public BookRepository(DataSource source)
            : base(source.Books)
        {
        }
    }
}
