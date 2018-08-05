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

    public class ClientService : IService<ClientDto>
    {
        private IUnitOfWork _uow;
        private IMapper _mapper;
        private IValidator<Client> _validator;

        public ClientService(IUnitOfWork uow, IMapper mapper, IValidator<Client> validator)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._validator = validator;
        }

        public ClientDto Get(Guid id)
        {
            var client = _uow.ClientRepository.Get(id);
            return _mapper.Map<Client, ClientDto>(client);
        }

        public List<ClientDto> GetAll()
        {
            var clients = _uow.ClientRepository.Get();
            return _mapper.Map<IEnumerable<Client>, List<ClientDto>>(clients);
        }

        public void Create(ClientDto dto)
        {
            var client = _mapper.Map<ClientDto, Client>(dto);

            client.Id = Guid.NewGuid();

            if (dto.WishListId != null)
            {
                client.WishList = (from wishBook in dto.WishListId
                                   join book in _uow.BookRepository.Get()
                                   on wishBook equals book.Id
                                   select book)?.ToList();
            }

            var validationResult = _validator.Validate(client);

            if (validationResult.IsValid)
            {
                _uow.ClientRepository.Create(client);
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public void Update(Guid id, ClientDto dto)
        {
            var client = _mapper.Map<ClientDto, Client>(dto);

            client.Id = id;

            if (dto.WishListId != null)
            {
                client.WishList = (from wishBook in dto.WishListId
                                   join book in _uow.BookRepository.Get()
                                   on wishBook equals book.Id
                                   select book)?.ToList();
            }

            var validationResult = _validator.Validate(client);

            if (validationResult.IsValid)
            {
                _uow.ClientRepository.Update(client);
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public void Delete(Guid id)
        {
            _uow.ClientRepository.Delete(id);
        }

        public ClientDto Find(ClientDto dto)
        {
            var client = _uow.ClientRepository.Find(x => (x.FirstName == dto.FirstName) && (x.SecondName == dto.SecondName)).FirstOrDefault();

            if (client == null)
            {
                throw new InvalidOperationException($"Can`t to find client with name: {dto.FirstName} {dto.SecondName}");
            }

            return _mapper.Map<Client, ClientDto>(client);
        }
    }
}
