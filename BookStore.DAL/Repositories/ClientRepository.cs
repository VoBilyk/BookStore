namespace BookStore.DAL.Repositories
{
    using BookStore.DAL.Models;

    public class ClientRepository : GenericRepository<Client>
    {
        public ClientRepository(DataSource source)
            : base(source.Clients)
        {
        }
    }
}
