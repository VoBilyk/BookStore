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
        private DataSource _db;

        private IBookRepository _bookRepository;
        private IClientRepository _clientRepository;
        private ICommentRepository _commentRepository;
        private IWishListRepository _wishListRepository;

        public UnitOfWork()
        {
            _db = new DataSource();
        }

        /// <inheritdoc/>
        public IBookRepository BookRepository => _bookRepository ?? new BookRepository(_db);

        /// <inheritdoc/>
        public IClientRepository ClientRepository => _clientRepository ?? new ClientRepository(_db);

        /// <inheritdoc/>
        public ICommentRepository CommentRepository => _commentRepository ?? new CommentRepository(_db);

        /// <inheritdoc/>
        public IWishListRepository WishListRepository => _wishListRepository ?? new WishListRepository(_db);
    }
}
