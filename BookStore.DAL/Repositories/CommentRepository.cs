namespace BookStore.DAL.Repositories
{
    using BookStore.DAL.Models;

    public class CommentRepository : GenericRepository<Comment>
    {
        public CommentRepository(DataSource source)
            : base(source.Comments)
        {
        }
    }
}
