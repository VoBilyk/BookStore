using BookStore.DAL.Models;

namespace BookStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Book> BookRepository { get; }

        IRepository<Client> ClientRepository { get; }

        IRepository<Comment> CommentRepository { get; }
    }
}
