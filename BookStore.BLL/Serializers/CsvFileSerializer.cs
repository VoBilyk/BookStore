namespace BookStore.BLL.Serializers
{
    using System.Collections.Generic;
    using System.IO;
    using ServiceStack;
    using ServiceStack.Text;

    using BookStore.BLL.Interfaces;

    /// <inheritdoc/>
    public class CsvFileSerializer : IFileSerializer
    {
        /// <inheritdoc/>
        public IList<TEntity> Read<TEntity>(string fileName)
        {
            var entities = new List<TEntity>();

            if (!File.Exists(fileName))
            {
                return entities;
            }

            entities = File.ReadAllText(fileName).FromCsv<List<TEntity>>();

            return entities;
        }

        /// <inheritdoc/>
        public void Write<TEntity>(IList<TEntity> entities, string fileName)
        {
            using (TextWriter streamWriter = File.AppendText(fileName))
            {
                CsvWriter<TEntity>.Write(streamWriter, entities);
                streamWriter.Close();
            }
        }
    }
}
