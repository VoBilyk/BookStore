using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    class BookCatalog
    {
        private static readonly Lazy<BookCatalog> lazy = new Lazy<BookCatalog>(() => new BookCatalog());
        public static BookCatalog Instance { get { return lazy.Value; } }

        public IEnumerable<Book> GetBooks { get { return books; } }

        private List<Book> books;

        private BookCatalog()
        {
            books = new List<Book>();
        }


        public void AddBook(Book book)
        {
            if (books.Contains(book))
            {
                throw new ArgumentException($"\"{book}\" already exists");
            }

            books.Add(book);
        }

        public void UpdateBook(Book oldBook, Book newBook)
        {
            if (!books.Contains(oldBook))
            {
                throw new ArgumentException($"Book doesn`t exist");
            }
            
            books.Find(item => item == oldBook).Update(newBook.Name, newBook.Author, newBook.Genre, newBook.Price);
        }

        public void RemoveBook(Book book)
        {
            if (!books.Contains(book))
            {
                throw new ArgumentException($"\"{book}\" doesn`t exist");
            }

            books.Remove(book);
        }


        public IEnumerable<Book> FindByName(string name)
        {
            return books.FindAll(book => book.Name == name);
        }

        public Book FindById(Guid id)
        {
            return books.Find(book => book.Id == id);
        }
    }
}
