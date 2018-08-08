namespace BookStore.DAL.Interfaces
{
    using BookStore.DAL.Models;

    /// <summary>
    /// Interface of patter UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets BookRepository
        /// </summary>
        IRepository<Book> BookRepository { get; }

        /// <summary>
        /// Gets ClientRepository
        /// </summary>
        IRepository<Client> ClientRepository { get; }

        /// <summary>
        /// Gets CommentRepository
        /// </summary>
        IRepository<Comment> CommentRepository { get; }

        /// <summary>
        /// Gets WishListRepository
        /// </summary>
        IRepository<Wish> WishListRepository { get; }
    }
}
