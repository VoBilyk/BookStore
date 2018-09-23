namespace BookStore.DAL.Interfaces
{
    using BookStore.DAL.Interfaces.Repositories;
    using BookStore.DAL.Models;

    /// <summary>
    /// Interface of patter UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets BookRepository
        /// </summary>
        IBookRepository BookRepository { get; }

        /// <summary>
        /// Gets ClientRepository
        /// </summary>
        IClientRepository ClientRepository { get; }

        /// <summary>
        /// Gets CommentRepository
        /// </summary>
        ICommentRepository CommentRepository { get; }

        /// <summary>
        /// Gets WishListRepository
        /// </summary>
        IWishListRepository WishListRepository { get; }
    }
}
