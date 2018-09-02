namespace BookStore.BLL.Serializers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    using BookStore.BLL.Interfaces;

    /// <inheritdoc/>
    public class BinaryFileSerializer : IFileSerializer
    {
        /// <inheritdoc/>
        public IList<TEntity> Read<TEntity>(string fileName)
        {
            var entities = new List<TEntity>();

            if (!File.Exists(fileName))
            {
                return entities;
            }

            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                var bf = new BinaryFormatter();
                entities = (List<TEntity>)bf.Deserialize(fs);
            }

            return entities;
        }

        /// <inheritdoc/>
        public void Write<TEntity>(IList<TEntity> entities, string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, entities);
            }
        }
    }
}
