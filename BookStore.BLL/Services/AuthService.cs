namespace BookStore.BLL.Services
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Microsoft.Extensions.Logging;

    using BookStore.BLL.Interfaces;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.Shared.DTOs;

    /// <inheritdoc/>
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly IClientService _clientService;

        private Guid? _currentUserId;

        public AuthService(
            IUnitOfWork uow,
            IMapper mapper,
            ILogger<AuthService> logger,
            IClientService clientService)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._logger = logger;
            this._clientService = clientService;
        }

        /// <inheritdoc/>
        public Guid? GetCurrentClientId() => _currentUserId;

        /// <inheritdoc/>
        public ClientDto GetCurrentClient()
        {
            if (!_currentUserId.HasValue)
            {
                return null;
            }

            var client = _clientService.Get(_currentUserId.Value);
            return client;
        }

        /// <inheritdoc/>
        public bool Login(ClientDto clientDto)
        {
            try
            {
                _currentUserId = _uow.ClientRepository
                    .Find(x => (x.FirstName == clientDto.FirstName) && (x.LastName == clientDto.LastName))?
                    .FirstOrDefault()?.Id;
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);
            }

            return _currentUserId.HasValue;
        }

        /// <inheritdoc/>
        public bool Logout()
        {
            _currentUserId = null;
            return !_currentUserId.HasValue;
        }
    }
}
