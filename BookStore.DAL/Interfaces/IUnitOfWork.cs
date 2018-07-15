using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Book> BookRepository { get; }

        IRepository<Client> ClientRepository { get; }

        IRepository<Comment> CommentRepository { get; }
    }
}
