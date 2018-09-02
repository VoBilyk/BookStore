namespace BookStore.BLL.Interfaces
{
    using System.Collections.Generic;

    public interface IFileSerializer
    {
        /// <summary>
        /// Read and deserialize data from file
        /// </summary>
        /// <typeparam name="TEntity">Entities which need to deserialize</typeparam>
        /// <param name="fileName">File name from where need deserialize data</param>
        /// <returns>Deserialized data</returns>
        IList<TEntity> Read<TEntity>(string fileName);

        /// <summary>
        /// Write and serialize data to file
        /// </summary>
        /// <typeparam name="TEntity">Entities which need to serialize</typeparam>
        /// <param name="entities">Collection which need to serialize to file</param>
        /// <param name="fileName">File where need to serialize data</param>
        void Write<TEntity>(IList<TEntity> entities, string fileName);
    }
}
