namespace BookStore.DAL.Interfaces.Repositories
{
    using BookStore.DAL.Models;

    /// <summary>
    /// Interface for book repository
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
    }
}
