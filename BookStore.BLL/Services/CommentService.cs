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

    public class CommentService : IService<CommentDto>
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        private IValidator<Comment> _validator;

        public CommentService(IUnitOfWork uow, IMapper mapper, IValidator<Comment> validator)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._validator = validator;
        }

        public CommentDto Get(Guid id)
        {
            var comment = _uow.CommentRepository.Get(id);
            return _mapper.Map<Comment, CommentDto>(comment);
        }

        public List<CommentDto> GetAll()
        {
            var comments = _uow.CommentRepository.Get();
            return _mapper.Map<IEnumerable<Comment>, List<CommentDto>>(comments);
        }

        public void Create(CommentDto dto)
        {
            //var comment = _mapper.Map<CommentDto, Comment>(dto);
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Client = _uow.ClientRepository.Get(dto.ClientId),
                Book = _uow.BookRepository.Get(dto.BookId)
            };

            var validationResult = _validator.Validate(comment);

            if (validationResult.IsValid)
            {
                _uow.CommentRepository.Create(comment);
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public void Update(Guid id, CommentDto dto)
        {
            //var comment = _mapper.Map<CommentDto, Comment>(dto);
            var comment = new Comment
            {
                Id = id,
                Client = _uow.ClientRepository.Get(dto.ClientId),
                Book = _uow.BookRepository.Get(dto.BookId)
            };

            var validationResult = _validator.Validate(comment);

            if (validationResult.IsValid)
            {
                _uow.CommentRepository.Update(comment);
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public void Delete(Guid id)
        {
            _uow.CommentRepository.Delete(id);
        }

        public CommentDto Find(CommentDto dto)
        {
            var comment = _uow.CommentRepository.Find(x => x.Text == dto.Text).FirstOrDefault();

            if (comment == null)
            {
                throw new InvalidOperationException($"Can`t to find comment");
            }

            return _mapper.Map<Comment, CommentDto>(comment);
        }
    }
}
