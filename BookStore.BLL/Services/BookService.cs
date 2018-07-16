using System;
using AutoMapper;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using BookStore.BLL.Interfaces;
using BookStore.DAL.Interfaces;
using BookStore.DAL.Models;
using BookStore.Shared.DTO;


namespace BookStore.BLL.Services
{
    public class BookService : IService<BookDto>
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        private AbstractValidator<Book> _validator;

        public BookService(IUnitOfWork uow, IMapper mapper, AbstractValidator<Book> validator)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._validator = validator;
        }

        public BookDto Get(Guid id)
        {
            var book = _uow.BookRepository.Get(id);
            return _mapper.Map<Book, BookDto>(book);
        }

        public List<BookDto> GetAll()
        {
            var books = _uow.BookRepository.Get();
            return _mapper.Map<IEnumerable<Book>, List<BookDto>>(books);
        }

        public void Create(BookDto dto)
        {
            var book = _mapper.Map<BookDto, Book>(dto);
            book.Id = Guid.NewGuid();

            var validationResult = _validator.Validate(book);

            if (validationResult.IsValid)
            {
                _uow.BookRepository.Create(book);
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            } 
        }

        public void Update(Guid id, BookDto dto)
        {
            var book = _mapper.Map<BookDto, Book>(dto);
            book.Id = id;

            var validationResult = _validator.Validate(book);

            if (validationResult.IsValid)
            {
                _uow.BookRepository.Update(book);
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public void Delete(Guid id)
        {
            _uow.BookRepository.Delete(id);
        }

        public void Delete()
        {
            _uow.BookRepository.Delete();
        }

        public BookDto Find(BookDto dto)
        {
            var book = _uow.BookRepository.Find(x => x.Name == dto.Name).FirstOrDefault();

            if(book == null)
            {
                throw new InvalidOperationException($"Can`t to find book with name: {dto.Name}");
            }

            return _mapper.Map<Book, BookDto>(book);
        }
    }
}
