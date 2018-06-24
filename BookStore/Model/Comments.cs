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

        public void AddComment(Client client, Book book, string text)
        {
            // User can leave only one comment.
            if(comments.FindAll(comment => comment.Book == book).Exists(comment => comment.Client == client))
            {
                throw new ArgumentException($"{client} have already left comment for book: {book}");
            }

            comments.Add(new Comment(client, book, text));
        }

        public void RemoveComment(Client client, Book book)
        {
            if (!comments.FindAll(comment => comment.Book == book).Exists(comment => comment.Client == client))
            {
                throw new ArgumentException($"{client} haven`t left comment for book: {book}");
            }

            comments.RemoveAll(comment => (comment.Client == client) && (comment.Book == book));
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
