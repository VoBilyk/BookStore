namespace BookStore.Tests.Services
{
    using System;
    using System.Linq;
    using NUnit.Framework;

    using BookStore.DAL;
    using BookStore.DAL.Models;
    using BookStore.DAL.Repositories;

    [TestFixture]
    public class BookRepositoryTest
    {
        private DataSource _db;

        [SetUp]
        public void Setup()
        {
            this._db = new DataSource();
        }

        [Test]
        public void GetAll_WhenRequestAllBook_ThenReturnAllSavedItems()
        {
            // Arrange
            var repository = new BookRepository(_db);

            // Act
            var returnedBooks = repository.Get().ToList();

            // Assert
            Assert.AreEqual(returnedBooks.Count, _db.Books.Count);
        }

        [Test]
        public void Get_WhenIdIsPassed_ThenReturnBookWithThisId()
        {
            // Arrange
            var bookId = _db.Books.First().Id;
            var repository = new BookRepository(_db);

            // Act
            var returnedBook = repository.Get(bookId);

            // Assert
            Assert.AreEqual(returnedBook.Id, bookId);
        }

        [Test]
        public void Find_WhenFindBookById_ThenReturnBookWithThisId()
        {
            // Arrange
            var bookId = _db.Books.First().Id;
            var repository = new BookRepository(_db);

            // Act
            var returnedBook = repository.Find(b => b.Id == bookId).FirstOrDefault();

            // Assert
            Assert.AreEqual(returnedBook?.Id, bookId);
        }

        [Test]
        public void Create_WhenCreateBook_ThenBecomeOneMoreInDb()
        {
            // Arrange
            var book = new Book();
            var amoutBeforeOperations = _db.Books.Count;
            var repository = new BookRepository(_db);

            // Act
            repository.Create(book);

            // Assert
            Assert.AreEqual(_db.Books.Count, amoutBeforeOperations + 1);
        }

        [Test]
        public void Update_WhenUpdateBook_ThenChangesWillBeInDb()
        {
            // Arrange
            var book = _db.Books.First();
            book.Name = "Name";

            var repository = new BookRepository(_db);

            // Act
            repository.Update(book);

            // Assert
            Assert.IsTrue(_db.Books.Where(b => b.Id == book.Id).First().Name == book.Name);
        }

        [Test]
        public void Delete_WhenDeleteBook_ThenAmountInDbMinusOne()
        {
            // Arrange
            var bookId = _db.Books.First().Id;
            var amoutBeforeOperations = _db.Books.Count;

            var repository = new BookRepository(_db);

            // Act
            repository.Delete(bookId);

            // Assert
            Assert.AreEqual(_db.Books.Count, amoutBeforeOperations - 1);
        }

        [Test]
        public void Delete_WhenDeleteBookByUnknownId_ThenThrowArgumentException()
        {
            // Arrange
            var bookId = Guid.NewGuid();

            var repository = new BookRepository(_db);

            // Act - Assert
            Assert.Throws<ArgumentException>(() => repository.Delete(bookId));
        }

        [Test]
        public void Get_WhenGetBookWithUnknownId_ThenThrowArgumentException()
        {
            // Arrange
            var bookId = default(Guid);

            var repository = new BookRepository(_db);

            // Act - Assert
            Assert.Throws<ArgumentException>(() => repository.Get(bookId));
        }

        [Test]
        public void Create_WhenCreateBookWithExistedId_ThenThrowArgumentException()
        {
            // Arrange
            var book = _db.Books.First();

            var repository = new BookRepository(_db);

            // Act - Assert
            Assert.Throws<ArgumentException>(() => repository.Create(book));
        }

        [Test]
        public void Update_WhenUpdateUnknownBook_ThenThrowArgumentException()
        {
            // Arrange
            var book = new Book();

            var repository = new BookRepository(_db);

            // Act - Assert
            Assert.Throws<ArgumentException>(() => repository.Update(book));
        }

        [Test]
        public void Find_WhenFindBookByUnknownId_ThenEmptyCollection()
        {
            // Arrange
            var bookId = default(Guid);

            var repository = new BookRepository(_db);

            // Act
            var foundBook = repository.Find(b => b.Id == bookId);

            // Act Assert
            Assert.IsEmpty(foundBook);
        }
    }
}