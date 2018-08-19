namespace BookStore.BLL.Interfaces
{
    using System;

    using BookStore.Shared.DTOs;

    /// <summary>
    /// Interface for authentication service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Getting logged client
        /// </summary>
        /// <returns>Logged client</returns>
        ClientDto GetCurrentClient();

        /// <summary>
        /// Getting only id of current client
        /// </summary>
        /// <returns>Id of current client</returns>
        Guid? GetCurrentClientId();

        /// <summary>
        /// To login used data from client dto
        /// </summary>
        /// <param name="client">Dto of client which wont to login</param>
        /// <returns>If operation success</returns>
        bool Login(ClientDto client);

        /// <summary>
        /// To logout current client
        /// </summary>
        /// <returns>If operation success</returns>
        bool Logout();
    }
}
