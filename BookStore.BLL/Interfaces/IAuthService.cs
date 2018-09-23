namespace BookStore.BLL.Interfaces
{
    using System;

    using BookStore.Shared.DTOs;

    /// <summary>
    /// Contract for authentication service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Getting authorized client
        /// </summary>
        /// <returns>Authorized client</returns>
        ClientDto GetCurrentClient();

        /// <summary>
        /// Getting current client id
        /// </summary>
        /// <returns>Current client id</returns>
        Guid? GetCurrentClientId();

        /// <summary>
        /// To login used data from client dto
        /// </summary>
        /// <param name="client">Dto of client which wants to login</param>
        /// <returns>If operation success</returns>
        bool Login(ClientDto client);

        /// <summary>
        /// To logout current client
        /// </summary>
        /// <returns>If logout success</returns>
        bool Logout();
    }
}
