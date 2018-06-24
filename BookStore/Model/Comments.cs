using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    class Comments
    {
        private static readonly Lazy<Comments> lazy = new Lazy<Comments>(() => new Comments());
        public static Comments Instance { get { return lazy.Value; } }

        private List<Comment> comments;

        public IEnumerable<Comment> GetComments { get { return comments; } }


        private Comments()
        {
            comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            // User can leave only one comment.
            if(comments.FindAll(comm => comm.Book == comment.Book).Exists(comm => comm.Client == comment.Client))
            {
                throw new ArgumentException($"{comment.Client} have already left comment for book: {comment.Book}");
            }

            comments.Add(comment);
        }

        public void RemoveComment(Comment comment)
        {
            if (!comments.Contains(comment))
            {
                throw new ArgumentException($"{comment.Client} haven`t left comment for book: {comment.Book}");
            }

            comments.Remove(comment);
        }


        public IEnumerable<Comment> FindByClient(Client client)
        {
            return comments.FindAll(comment => comment.Client == client);
        }

        public IEnumerable<Comment> FindByBook(Book book)
        {
            return comments.FindAll(comment => comment.Book == book);
        }
    }
}
