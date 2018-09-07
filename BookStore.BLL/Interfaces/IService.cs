namespace BookStore.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contract for service with business logic for working with item
    /// </summary>
    /// <typeparam name="TDto">Data transfer object</typeparam>
    public interface IService<TDto>
    {
        /// <summary>
        /// Getting item by id
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Item with searching id</returns>
        TDto Get(Guid id);

        /// <summary>
        /// Getting all items
        /// </summary>
        /// <returns>List of items</returns>
        List<TDto> GetAll();

        /// <summary>
        /// Finding item
        /// </summary>
        /// <param name="query">Value which need to find</param>
        /// <returns>Found items</returns>
        List<TDto> Find(string query);

        /// <summary>
        /// Creating item
        /// </summary>
        /// <param name="dto">Data transfer object of item which need to create</param>
        void Create(TDto dto);

        /// <summary>
        /// Updating item by id
        /// </summary>
        /// <param name="id">Item id which need update</param>
        /// <param name="dto">Data transfer object of item which need to update</param>
        void Update(Guid id, TDto dto);

        /// <summary>
        /// Deleting item by id
        /// </summary>
        /// <param name="id">Item id which need to delete</param>
        void Delete(Guid id);
    }
}
