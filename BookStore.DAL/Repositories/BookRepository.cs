using System;
using System.Collections.Generic;
using BookStore.DAL.Models;

namespace BookStore.DAL.Repositories
{
    public class BookRepository : GenericRepository<Book>
    {
        public BookRepository(List<Book> context) : base(context) { }
    }
}
