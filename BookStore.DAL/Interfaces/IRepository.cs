namespace BookStore.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface of pattern repository
    /// </summary>
    /// <typeparam name="TEntity">Entity which implements IEntity interface</typeparam>
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Getting all elements
        /// </summary>
        /// <returns>List of all elements</returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Getting element with specified id
        /// </summary>
        /// <param name="id">Element id which need to get</param>
        /// <returns>Element which specified id</returns>
        TEntity Get(Guid id);

        /// <summary>
        /// Finding element with specified lambda
        /// </summary>
        /// <param name="predicate">Lambda predicate</param>
        /// <returns>List of found elements</returns>
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        /// <summary>
        /// Creating element by specified id
        /// </summary>
        /// <param name="entity">Element which need to update</param>
        void Create(TEntity entity);

        /// <summary>
        /// Updating element by specified id
        /// </summary>
        /// <param name="entity">Element which need to update</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deleting element by specified id
        /// </summary>
        /// <param name="id">Element id which need delete</param>
        void Delete(Guid id);
    }
}
