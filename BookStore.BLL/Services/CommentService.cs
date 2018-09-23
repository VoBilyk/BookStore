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
    using BookStore.Shared.Extensions;

    /// <inheritdoc/>
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<Comment> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        /// <param name="uow">UnitOfWork implementation</param>
        /// <param name="mapper">Mapper implementation</param>
        /// <param name="validator">Validator implementation</param>
        public CommentService(IUnitOfWork uow, IMapper mapper, IValidator<Comment> validator)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._validator = validator;
        }

        /// <inheritdoc/>
        public CommentDto Get(Guid id)
        {
            var comment = _uow.CommentRepository.Get(id);
            return _mapper.Map<Comment, CommentDto>(comment);
        }

        /// <inheritdoc/>
        public List<CommentDto> GetAll()
        {
            var comments = _uow.CommentRepository.Get();
            return _mapper.Map<IEnumerable<Comment>, List<CommentDto>>(comments);
        }

        /// <inheritdoc/>
        public List<CommentDto> Find(string query)
        {
            var comments = _uow.CommentRepository
                .Find(x => x.Text.ContainsIgnoringCase(query))
                .ToList();

            return _mapper.Map<List<Comment>, List<CommentDto>>(comments);
        }

        /// <inheritdoc/>
        public void Create(CommentDto dto)
        {
            var comment = _mapper.Map<CommentDto, Comment>(dto);
            comment.Id = Guid.NewGuid();
            comment.Client = _uow.ClientRepository.Get(dto.ClientId);
            comment.Book = _uow.BookRepository.Get(dto.BookId);

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

        /// <inheritdoc/>
        public void Update(Guid id, CommentDto dto)
        {
            var comment = _mapper.Map<CommentDto, Comment>(dto);
            comment.Id = id;
            comment.Client = _uow.ClientRepository.Get(dto.ClientId);
            comment.Book = _uow.BookRepository.Get(dto.BookId);

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

        /// <inheritdoc/>
        public void Delete(Guid id)
        {
            _uow.CommentRepository.Delete(id);
        }
    }
}
