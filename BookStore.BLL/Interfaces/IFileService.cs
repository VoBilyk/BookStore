namespace BookStore.BLL.Interfaces
{
    /// <summary>
    /// Contract for file services
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Save data to file using serializer
        /// </summary>
        /// <param name="fileName">File in which need to write</param>
        /// <param name="serializer">Serializer</param>
        void SaveToFile(string fileName, IFileSerializer serializer = null);

        /// <summary>
        /// Read data from file using serializer
        /// </summary>
        /// <param name="fileName">File which need to read</param>
        /// <param name="serializer">Serializer</param>
        void ReadFromFile(string fileName, IFileSerializer serializer = null);
    }
}
