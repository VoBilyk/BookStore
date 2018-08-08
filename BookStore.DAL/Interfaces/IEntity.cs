namespace BookStore.DAL.Interfaces
{
    using System;

    /// <summary>
    /// Interface for entities
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets gets entity id
        /// </summary>
        Guid Id { get; set; }
    }
}
