using System;
using System.Collections.Generic;
using System.Text;
using BookStore.DAL.Interfaces;
using BookStore.DAL.Models;
using BookStore.DAL.Repositories;

namespace BookStore.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataSource db = new DataSource();

        private IRepository<Book> _bookRepository;
        private IRepository<Client> _clientRepository;
        private IRepository<Comment> _commentRepository;


        public IRepository<Book> BookRepository
        {
            get
            {
                if (_bookRepository == null)
                {
                    _bookRepository = new BookRepository(db.Books);
                }
                return _bookRepository;
            }
        }

        public IRepository<Client> ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(db.Clients);
                }
                return _clientRepository;
            }
        }

        public IRepository<Comment> CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                {
                    _commentRepository = new CommentRepository(db.Comments);
                }
                return _commentRepository;
            }
        }
    }
}
