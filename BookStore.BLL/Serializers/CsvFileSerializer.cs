namespace BookStore.BLL.Serializers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ServiceStack;
    using ServiceStack.Text;

    using BookStore.BLL.Interfaces;

    /// <inheritdoc/>
    public class CsvFileSerializer : IFileSerializer
    {
        /// <inheritdoc/>
        public string Name { get; set; } = "csv";

        /// <inheritdoc/>
        public IEnumerable<TEntity> Read<TEntity>(string fileName)
        {
            var entities = Enumerable.Empty<TEntity>();

            if (!File.Exists(fileName))
            {
                return entities;
            }

            entities = File.ReadAllText(fileName).FromCsv<IEnumerable<TEntity>>();

            return entities;
        }

        /// <inheritdoc/>
        public void Write<TEntity>(IEnumerable<TEntity> entities, string fileName)
        {
            using (TextWriter streamWriter = File.AppendText(fileName))
            {
                CsvWriter<TEntity>.Write(streamWriter, entities);
                streamWriter.Close();
            }
        }
    }
}
