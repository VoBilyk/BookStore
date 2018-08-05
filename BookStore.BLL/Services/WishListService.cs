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

    /// <summary>
    /// Service which implements business logic for wishList
    /// </summary>
    public class WishListService : IService<WishDto>
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        private IValidator<Wish> _validator;

        public WishListService(IUnitOfWork uow, IMapper mapper, IValidator<Wish> validator)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._validator = validator;
        }

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

        public void Delete(Guid id)
        {
            _uow.WishListRepository.Delete(id);
        }

        public WishDto Find(WishDto dto)
        {
            var wish = _uow.WishListRepository
                .Find(x => x.BookId == dto.BookId && x.ClientId == dto.ClientId)
                .FirstOrDefault();

            if (wish == null)
            {
                throw new InvalidOperationException($"Can`t to wish comment");
            }

            return _mapper.Map<Wish, WishDto>(wish);
        }
    }
}
