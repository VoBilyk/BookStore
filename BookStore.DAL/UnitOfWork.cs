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

        /// <inheritdoc/>
        public IRepository<Book> BookRepository => _bookRepository ?? new BookRepository(_db);

        /// <inheritdoc/>
        public IRepository<Client> ClientRepository => _clientRepository ?? new ClientRepository(_db);

        /// <inheritdoc/>
        public IRepository<Comment> CommentRepository => _commentRepository ?? new CommentRepository(_db);

        /// <inheritdoc/>
        public IRepository<Wish> WishListRepository => _wishListRepository ?? new WishListRepository(_db);
    }
}
