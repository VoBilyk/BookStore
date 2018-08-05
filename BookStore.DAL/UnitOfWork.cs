namespace BookStore.DAL
{
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.DAL.Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private DataSource _db;

        private IRepository<Book> _bookRepository;
        private IRepository<Client> _clientRepository;
        private IRepository<Comment> _commentRepository;

        public UnitOfWork()
        {
            _db = new DataSource();
        }

        public IRepository<Book> BookRepository
        {
            get
            {
                if (_bookRepository == null)
                {
                    _bookRepository = new GenericRepository<Book>(_db.Books);
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
                    _clientRepository = new GenericRepository<Client>(_db.Clients);
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
                    _commentRepository = new GenericRepository<Comment>(_db.Comments);
                }

                return _commentRepository;
            }
        }
    }
}
