namespace BookStore.BLL.Serializers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    using BookStore.BLL.Interfaces;

    /// <inheritdoc/>
    public class XmlFileSerializer : IFileSerializer
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
                var xs = new XmlSerializer(typeof(IList<TEntity>));
                entities = (List<TEntity>)xs.Deserialize(fs);
            }

            return entities;
        }

        /// <inheritdoc/>
        public void Write<TEntity>(IList<TEntity> entities, string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var xs = new XmlSerializer(typeof(IList<TEntity>));
                xs.Serialize(fs, entities);
            }
        }
    }
}
