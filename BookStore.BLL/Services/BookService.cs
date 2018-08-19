namespace BookStore.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using FluentValidation;

    using BookStore.BLL.Interfaces;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.Shared.DTOs;

    /// <inheritdoc/>
    public class BookService : IBookService
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        private IValidator<Book> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="uow">UnitOfWork implementation</param>
        /// <param name="mapper">Mapper implementation</param>
        /// <param name="validator">Validator implementation</param>
        public BookService(IUnitOfWork uow, IMapper mapper, IValidator<Book> validator)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._validator = validator;
        }

        /// <inheritdoc/>
        public BookDto Get(Guid id)
        {
            var book = _uow.BookRepository.Get(id);
            var bookDto = _mapper.Map<Book, BookDto>(book);

            bookDto.WishedClientsId = _uow.WishListRepository
                .Find(w => w.BookId == id)
                .Select(w => w.ClientId)
                .ToList();

            bookDto.UserCommentsId = _uow.CommentRepository
                .Find(c => c.Book.Id == id)
                .Select(c => c.Id)
                .ToList();

            return bookDto;
        }

        /// <inheritdoc/>
        public List<BookDto> GetAll()
        {
            var books = _uow.BookRepository.Get();
            var booksDto = _mapper.Map<IEnumerable<Book>, List<BookDto>>(books);

            foreach (var bookDto in booksDto)
            {
                bookDto.WishedClientsId = _uow.WishListRepository
                .Find(w => w.BookId == bookDto.Id)
                .Select(w => w.ClientId)
                .ToList();

                bookDto.UserCommentsId = _uow.CommentRepository
                .Find(c => c.Book.Id == bookDto.Id)
                .Select(c => c.Id)
                .ToList();
            }

            return booksDto;
        }

        /// <inheritdoc/>
        public BookDto Find(BookDto dto)
        {
            var book = _uow.BookRepository.Find(x => x.Name == dto.Name)?.FirstOrDefault();

            if (book == null)
            {
                throw new ArgumentException($"Can`t to find book with name: {dto.Name}");
            }

            return _mapper.Map<Book, BookDto>(book);
        }

        /// <inheritdoc/>
        public void Create(BookDto dto)
        {
            var book = _mapper.Map<BookDto, Book>(dto);
            book.Id = Guid.NewGuid();

            var validationResult = _validator.Validate(book);

            if (validationResult.IsValid)
            {
                _uow.BookRepository.Create(book);

                foreach (var clientId in dto.WishedClientsId)
                {
                    _uow.WishListRepository.Create(
                        new Wish
                        {
                            ClientId = clientId,
                            BookId = book.Id
                        });
                }
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        /// <inheritdoc/>
        public void Update(Guid id, BookDto dto)
        {
            var book = _mapper.Map<BookDto, Book>(dto);
            book.Id = id;

            var validationResult = _validator.Validate(book);

            if (validationResult.IsValid)
            {
                _uow.BookRepository.Update(book);

                foreach (var clientId in dto.WishedClientsId)
                {
                    _uow.WishListRepository.Create(
                        new Wish
                        {
                            ClientId = clientId,
                            BookId = book.Id
                        });
                }
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        /// <inheritdoc/>
        public void Delete(Guid id)
        {
            _uow.BookRepository.Delete(id);
        }
    }
}
