using System;
using System.Collections.Generic;
using BookStore.DAL.Models;

namespace BookStore.DAL.Repositories
{
    public class CommentRepository : GenericRepository<Comment>
    {
        public CommentRepository(List<Comment> context) : base(context) { }
    }
}
