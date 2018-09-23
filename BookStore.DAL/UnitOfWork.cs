namespace BookStore.DAL
{
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Interfaces.Repositories;
    using BookStore.DAL.Repositories;

    /// <summary>
    /// Class which implemented IUnitOfWork interface
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataSource _db;

        private IBookRepository _bookRepository;
        private IClientRepository _clientRepository;
        private ICommentRepository _commentRepository;
        private IWishListRepository _wishListRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="data">Data source context</param>
        public UnitOfWork(DataSource data)
        {
            this._db = data;
        }

        /// <inheritdoc/>
        public IBookRepository BookRepository
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

        /// <inheritdoc/>
        public IClientRepository ClientRepository
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

        /// <inheritdoc/>
        public ICommentRepository CommentRepository
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

        /// <inheritdoc/>
        public IWishListRepository WishListRepository
        {
            get
            {
                if (_wishListRepository == null)
                {
                    _wishListRepository = new WishListRepository(_db);
                }

                return _wishListRepository;
            }
        }
    }
}
