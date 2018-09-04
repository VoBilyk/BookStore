namespace BookStore.Tests.Repositories
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using NUnit.Framework;

    using BookStore.DAL;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.DAL.Repositories;

    [TestFixture]
    public class UnitOfWorkTest
    {
        private DataSource _db;

        [SetUp]
        public void Setup()
        {
            this._db = A.Fake<DataSource>();
        }

        [Test]
        public void GetRepository_WhenGettingWIshListRepository_ThenReturnNotNull()
        {
            // Arrange
            var uow = new UnitOfWork(_db);

            // Act
            var repository = uow.WishListRepository;

            // Assert
            Assert.IsNotNull(repository);
        }

        [Test]
        public void GetRepository_WhenGettingBookRepository_ThenReturnNotNull()
        {
            // Arrange
            var uow = new UnitOfWork(_db);

            // Act
            var repository = uow.BookRepository;

            // Assert
            Assert.IsNotNull(repository);
        }

        [Test]
        public void GetRepository_WhenGettingClientRepository_ThenReturnNotNull()
        {
            // Arrange
            var uow = new UnitOfWork(_db);

            // Act
            var repository = uow.ClientRepository;

            // Assert
            Assert.IsNotNull(repository);
        }

        [Test]
        public void GetRepository_WhenGettingCommentRepository_ThenReturnNotNull()
        {
            // Arrange
            var uow = new UnitOfWork(_db);

            // Act
            var repository = uow.CommentRepository;

            // Assert
            Assert.IsNotNull(repository);
        }
    }
}