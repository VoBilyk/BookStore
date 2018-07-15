using System;
using System.Collections.Generic;
using BookStore.DAL.Models;

namespace BookStore.DAL.Repositories
{
    public class ClientRepository : GenericRepository<Client>
    {
        public ClientRepository(List<Client> context) : base(context) { }
    }
}
