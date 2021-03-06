﻿namespace BookStore.BLL.Services
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
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<Client> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="uow">UnitOfWork implementation</param>
        /// <param name="mapper">Mapper implementation</param>
        /// <param name="validator">Validator implementation</param>
        public ClientService(IUnitOfWork uow, IMapper mapper, IValidator<Client> validator)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._validator = validator;
        }

        /// <inheritdoc/>
        public List<ClientDto> Find(string query)
        {
            var clients = _uow.ClientRepository
                .Find(x => x.FirstName.ContainsIgnoringCase(query) ||
                           x.LastName.ContainsIgnoringCase(query))
                .ToList();

            return _mapper.Map<List<Client>, List<ClientDto>>(clients);
        }

        /// <inheritdoc/>
        public ClientDto Get(Guid id)
        {
            var client = _uow.ClientRepository.Get(id);
            var clientDto = _mapper.Map<Client, ClientDto>(client);

            clientDto.WishedBooksId = _uow.WishListRepository
                .Find(w => w.ClientId == id)
                .Select(w => w.BookId)
                .ToList();

            clientDto.CommentsId = _uow.CommentRepository
                .Find(c => c.Client.Id == id)
                .Select(c => c.Id)
                .ToList();

            return clientDto;
        }

        /// <inheritdoc/>
        public List<ClientDto> GetAll()
        {
            var clients = _uow.ClientRepository.Get();
            var clientsDto = _mapper.Map<IEnumerable<Client>, List<ClientDto>>(clients);

            foreach (var clientDto in clientsDto)
            {
                clientDto.WishedBooksId = _uow.WishListRepository
                .Find(w => w.ClientId == clientDto.Id)
                .Select(w => w.BookId)
                .ToList();

                clientDto.CommentsId = _uow.CommentRepository
                .Find(c => c.Client.Id == clientDto.Id)
                .Select(c => c.Id)
                .ToList();
            }

            return clientsDto;
        }

        /// <inheritdoc/>
        public void Create(ClientDto dto)
        {
            var client = _mapper.Map<ClientDto, Client>(dto);
            client.Id = Guid.NewGuid();

            var validationResult = _validator.Validate(client);

            if (validationResult.IsValid)
            {
                _uow.ClientRepository.Create(client);

                foreach (var bookId in dto.WishedBooksId)
                {
                    _uow.WishListRepository.Create(
                        new Wish
                        {
                            ClientId = client.Id,
                            BookId = bookId
                        });
                }
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        /// <inheritdoc/>
        public void Update(Guid id, ClientDto dto)
        {
            var client = _mapper.Map<ClientDto, Client>(dto);
            client.Id = id;

            var validationResult = _validator.Validate(client);

            if (validationResult.IsValid)
            {
                _uow.ClientRepository.Update(client);

                foreach (var bookId in dto.WishedBooksId)
                {
                    _uow.WishListRepository.Create(
                        new Wish
                        {
                            ClientId = client.Id,
                            BookId = bookId
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
            _uow.ClientRepository.Delete(id);
        }
    }
}
