﻿namespace BookStore.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using FakeItEasy;
    using FluentValidation;
    using FluentValidation.Results;
    using NUnit.Framework;

    using BookStore.BLL.MappingProfiles;
    using BookStore.BLL.Services;
    using BookStore.BLL.Validators;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.Shared.DTOs;

    [TestFixture]
    public class BookServiceTest
    {
        private IUnitOfWork _unitOfWorkFake;
        private IMapper _mapperFake;
        private IMapper _mapper;
        private IValidator<Book> _validator;
        private IValidator<Book> _alwaysValidValidator;

        private BookDto bookDto;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            this._mapper = config.CreateMapper();
            this._unitOfWorkFake = A.Fake<IUnitOfWork>();
            this._mapperFake = A.Fake<IMapper>();
            this._validator = new BookValidator();

            this._alwaysValidValidator = A.Fake<IValidator<Book>>();
            A.CallTo(() => _alwaysValidValidator.Validate(A<Book>._)).Returns(new ValidationResult());

            this.bookDto = new BookDto
            {
                Name = "BookName",
                Author = "Author",
                Genre = "Genre",
                WishedClientsId = new List<Guid> { Guid.NewGuid() },
                UserCommentsId = new List<Guid> { Guid.NewGuid() }
            };

        }

        [Test]
        public void GetAll_WhenRequestAllBooks_ThenReturnAllSavedBooks()
        {
            // Arrange
            var books = new List<Book>() { new Book { }, new Book { } };

            A.CallTo(() => _unitOfWorkFake.BookRepository.Get()).Returns(books);

            var service = new BookService(_unitOfWorkFake, _mapper, _alwaysValidValidator);

            // Act
            var returnedBooks = service.GetAll();

            // Assert
            Assert.AreEqual(returnedBooks.Count, books.Count);
        }

        [Test]
        public void Get_WhenIdIsPassed_ThenReturnBookWithThisId()
        {
            // Arrange
            var bookId = Guid.NewGuid();

            A.CallTo(() => _unitOfWorkFake.BookRepository.Get(bookId))
                .Returns(new Book { Id = bookId });

            var service = new BookService(_unitOfWorkFake, _mapper, _alwaysValidValidator);

            // Act
            var returnedBook = service.Get(bookId);

            // Assert
            Assert.AreEqual(returnedBook.Id, bookId);
        }

        [Test]
        public void Find_WhenFindBook_ThenReturnNeeded()
        {
            // Arrange
            var book = new Book { Name = "BookName" };

            var service = new BookService(_unitOfWorkFake, _mapper, _validator);
            A.CallTo(() => _unitOfWorkFake.BookRepository.Find(A<Func<Book,bool>>._))
                .Returns(new List<Book>{ book });

            // Act
            var returnedBook = service.Find(bookDto);

            // Assert
            Assert.AreEqual(bookDto.Name, returnedBook.Name);
        }

        [Test]
        public void Find_WhenFindUnknownBook_ThenThrowInvalidOperationExeption()
        {
            // Arrange
            var book = new BookDto();

            var service = new BookService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<InvalidOperationException>(() => service.Find(book));
        }

        [Test]
        public void Create_WhenBookIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var book = new BookDto();

            var service = new BookService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<ValidationException>(() => service.Create(book));
        }

        [Test]
        public void Update_WhenBookIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var book = new BookDto();
            var bookId = Guid.NewGuid();

            var service = new BookService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<ValidationException>(() => service.Update(bookId, book));
        }

        [Test]
        public void Delete_WhenDeleteBook_ThenInvokeDeleteByRepository()
        {
            // Arrange
            var bookId = Guid.NewGuid();

            var service = new BookService(_unitOfWorkFake, _mapper, _validator);
            service.Delete(bookId);

            // Act - Assert
            A.CallTo(() => _unitOfWorkFake.BookRepository.Delete(bookId)).MustHaveHappened();
        }

        [Test]
        public void Create_WhenGetBook_ThenInvokedMapper()
        {
            // Arrange
            var book = new Book { Name = "Name", Author = "Author", Genre = "Genre", Price = 10};

            A.CallTo(() => _mapperFake.Map<BookDto, Book>(bookDto)).Returns(book);
            var service = new BookService(_unitOfWorkFake, _mapperFake, _validator);
            service.Create(bookDto);

            // Act - Assert
            A.CallTo(() => _mapperFake.Map<BookDto, Book>(bookDto)).MustHaveHappened();
        }
    }
}