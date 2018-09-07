namespace BookStore.BLL.Services
{
    using System;
    using System.Linq;

    using BookStore.BLL.Interfaces;
    using BookStore.DAL.Interfaces;
    using BookStore.Shared.DTOs;

    /// <inheritdoc/>
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IClientService _clientService;

        private Guid? _currentUserId;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="uow">UnitOfWork implementation</param>
        /// <param name="clientService">Client service implementation</param>
        public AuthService(
            IUnitOfWork uow,
            IClientService clientService)
        {
            this._uow = uow;
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
        public bool Login(ClientDto client)
        {
            _currentUserId = _uow.ClientRepository
                .Find(x => (x.FirstName == client.FirstName) && (x.LastName == client.LastName))?
                .FirstOrDefault()?.Id;

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
