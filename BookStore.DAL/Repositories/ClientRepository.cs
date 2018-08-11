namespace BookStore.DAL.Repositories
{
    using BookStore.DAL.Interfaces.Repositories;
    using BookStore.DAL.Models;

    /// <inheritdoc/>
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRepository"/> class.
        /// </summary>
        /// <param name="source">Data source</param>
        public ClientRepository(DataSource source)
            : base(source.Clients)
        {
        }
    }
}
