namespace BookStore.BLL.Serializers
{
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    using BookStore.BLL.Interfaces;

    /// <inheritdoc/>
    public class JsonFileSerializer : IFileSerializer
    {
        /// <inheritdoc/>
        public IList<TEntity> Read<TEntity>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return new List<TEntity>();
            }

            var content = File.ReadAllText(fileName);

            var entities = JsonConvert.DeserializeObject<IList<TEntity>>(content);

            return entities;
        }

        /// <inheritdoc/>
        public void Write<TEntity>(IList<TEntity> entities, string fileName)
        {
            var serializedData = JsonConvert.SerializeObject(entities);

            File.WriteAllText(fileName, serializedData);
        }
    }
}
