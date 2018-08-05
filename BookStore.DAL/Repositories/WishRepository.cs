namespace BookStore.DAL.Repositories
{
    using BookStore.DAL.Models;

    public class WishRepository : GenericRepository<Wish>
    {
        public WishRepository(DataSource source)
            : base(source.WishList)
        {
        }
    }
}
