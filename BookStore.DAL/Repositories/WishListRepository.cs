namespace BookStore.DAL.Repositories
{
    using BookStore.DAL.Interfaces.Repositories;
    using BookStore.DAL.Models;

    /// <inheritdoc/>
    public class WishListRepository : GenericRepository<Wish>, IWishListRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WishListRepository"/> class.
        /// </summary>
        /// <param name="source">Data source</param>
        public WishListRepository(DataSource source)
            : base(source.WishList)
        {
        }
    }
}
