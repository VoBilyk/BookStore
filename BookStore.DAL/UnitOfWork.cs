namespace BookStore.DAL
{
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.DAL.Repositories;

    /// <summary>
    /// Class which implemented IUnitOfWork interface
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private DataSource _db;

        private IRepository<Book> _bookRepository;
        private IRepository<Client> _clientRepository;
        private IRepository<Comment> _commentRepository;
        private IRepository<Wish> _wishListRepository;

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
                    _bookRepository = new BookRepository(_db);
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
                    _clientRepository = new ClientRepository(_db);
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
                    _commentRepository = new CommentRepository(_db);
                }

                return _commentRepository;
            }
        }

        public IRepository<Wish> WishListRepository
        {
            get
            {
                if (_wishListRepository == null)
                {
                    _wishListRepository = new WishRepository(_db);
                }

                return _wishListRepository;
            }
        }
    }
}
