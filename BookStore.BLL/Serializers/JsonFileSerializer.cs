namespace BookStore.BLL.Serializers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;

    using BookStore.BLL.Interfaces;

    /// <inheritdoc/>
    public class JsonFileSerializer : IFileSerializer
    {
        /// <inheritdoc/>
        public string Name { get; set; } = "json";

        /// <inheritdoc/>
        public IEnumerable<TEntity> Read<TEntity>(string fileName)
        {
            var entities = Enumerable.Empty<TEntity>();

            if (!File.Exists(fileName))
            {
                return entities;
            }

            var content = File.ReadAllText(fileName);

            entities = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(content);

            return entities;
        }

        /// <inheritdoc/>
        public void Write<TEntity>(IEnumerable<TEntity> entities, string fileName)
        {
            var serializedData = JsonConvert.SerializeObject(entities);

            File.WriteAllText(fileName, serializedData);
        }
    }
}
