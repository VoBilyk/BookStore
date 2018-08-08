namespace BookStore.DAL.Repositories
{
    using BookStore.DAL.Models;

    /// <inheritdoc/>
    public class CommentRepository : GenericRepository<Comment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepository"/> class.
        /// </summary>
        /// <param name="source">Data source</param>
        public CommentRepository(DataSource source)
            : base(source.Comments)
        {
        }
    }
}
