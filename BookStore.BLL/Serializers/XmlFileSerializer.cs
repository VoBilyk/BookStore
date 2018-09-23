using System;

namespace BookStore.BLL.Serializers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using BookStore.BLL.Interfaces;

    /// <inheritdoc/>
    public class XmlFileSerializer : IFileSerializer
    {
        /// <inheritdoc/>
        public string Name { get; set; } = "xml";

        /// <inheritdoc/>
        public IEnumerable<TEntity> Read<TEntity>(string fileName)
        {
            var entities = Enumerable.Empty<TEntity>();

            if (!File.Exists(fileName))
            {
                return entities;
            }

            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                var xs = new XmlSerializer(typeof(List<TEntity>));
                entities = (IEnumerable<TEntity>)xs.Deserialize(fs);
            }

            return entities;
        }

        /// <inheritdoc/>
        public void Write<TEntity>(IEnumerable<TEntity> entities, string fileName)
        {
            if (!(entities is List<TEntity>))
            {
                throw new NotSupportedException("Xml serializer works only with list");
            }

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var xs = new XmlSerializer(typeof(List<TEntity>));
                xs.Serialize(fs, entities);
            }
        }
    }
}
