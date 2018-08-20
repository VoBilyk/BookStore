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
    public class WishListService : IWishListService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<Wish> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="WishListService"/> class.
        /// </summary>
        /// <param name="uow">UnitOfWork implementation</param>
        /// <param name="mapper">Mapper implementation</param>
        /// <param name="validator">Validator implementation</param>
        public WishListService(IUnitOfWork uow, IMapper mapper, IValidator<Wish> validator)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._validator = validator;
        }

        /// <inheritdoc/>
        public WishDto Get(Guid id)
        {
            var wish = _uow.WishListRepository.Get(id);
            return _mapper.Map<Wish, WishDto>(wish);
        }

        public List<WishDto> GetAll()
        {
            var wishList = _uow.WishListRepository.Get();
            return _mapper.Map<IEnumerable<Wish>, List<WishDto>>(wishList);
        }

        /// <inheritdoc/>
        public WishDto Find(WishDto dto)
        {
            var wish = _uow.WishListRepository
                .Find(x => x.BookId == dto.BookId && x.ClientId == dto.ClientId)
                ?.FirstOrDefault();

            if (wish == null)
            {
                throw new ArgumentException("Can`t find wish");
            }

            return _mapper.Map<Wish, WishDto>(wish);
        }

        /// <inheritdoc/>
        public void Create(WishDto dto)
        {
            var wish = _mapper.Map<WishDto, Wish>(dto);
            wish.Id = Guid.NewGuid();

            var validationResult = _validator.Validate(wish);

            if (validationResult.IsValid)
            {
                _uow.WishListRepository.Create(wish);
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        /// <inheritdoc/>
        public void Update(Guid id, WishDto dto)
        {
            var wish = _mapper.Map<WishDto, Wish>(dto);
            wish.Id = id;

            var validationResult = _validator.Validate(wish);

            if (validationResult.IsValid)
            {
                _uow.WishListRepository.Update(wish);
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        /// <inheritdoc/>
        public void Delete(Guid id)
        {
            _uow.WishListRepository.Delete(id);
        }
    }
}
