namespace BookStore.DAL.Interfaces
{
    using BookStore.DAL.Models;

    public interface IUnitOfWork
    {
        IRepository<Book> BookRepository { get; }

        IRepository<Client> ClientRepository { get; }

        IRepository<Comment> CommentRepository { get; }

        IRepository<Wish> WishListRepository { get; }
    }
}
