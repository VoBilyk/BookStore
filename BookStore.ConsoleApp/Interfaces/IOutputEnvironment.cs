namespace BookStore.ConsoleApp.Interfaces
{
    /// <summary>
    /// Interface for writing to and reading from environment
    /// </summary>
    public interface IOutputEnvironment
    {
        /// <summary>
        /// Reading line from environment
        /// </summary>
        /// <returns>Read line</returns>
        string Read();

        /// <summary>
        /// Reading key from environment
        /// </summary>
        void ReadKey();

        /// <summary>
        /// Getting entered value
        /// </summary>
        /// <returns>Entered value</returns>
        int ReadInt();

        /// <summary>
        /// Read int value with thresholds
        /// </summary>
        /// <param name="min">Min possible value</param>
        /// <param name="max">Max possible value</param>
        /// <returns>Entered value</returns>
        int ReadInt(int min, int max);

        /// <summary>
        /// Writing text to environment
        /// </summary>
        /// <param name="str">Text which need to write</param>
        void Write(string str);

        /// <summary>
        /// Writing text line to environment
        /// </summary>
        /// <param name="str">Text which need to write</param>
        void WriteLine(string str);

        /// <summary>
        /// Clearing text from environment
        /// </summary>
        void Clear();
    }
}
