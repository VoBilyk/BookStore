namespace BookStore.BLL.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Contract for file serializer
    /// </summary>
    public interface IFileSerializer
    {
        /// <summary>
        /// Gets or sets serializer name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Read and deserialize data from file
        /// </summary>
        /// <typeparam name="TEntity">Entities which need to deserialize</typeparam>
        /// <param name="fileName">File name from where need deserialize data</param>
        /// <returns>Deserialized data</returns>
        IEnumerable<TEntity> Read<TEntity>(string fileName);

        /// <summary>
        /// Write and serialize data to file
        /// </summary>
        /// <typeparam name="TEntity">Entities which need to serialize</typeparam>
        /// <param name="entities">Collection which need to serialize to file</param>
        /// <param name="fileName">File where need to serialize data</param>
        void Write<TEntity>(IEnumerable<TEntity> entities, string fileName);
    }
}
