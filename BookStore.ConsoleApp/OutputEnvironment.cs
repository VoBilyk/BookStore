namespace BookStore.ConsoleApp
{
    using System;

    using BookStore.ConsoleApp.Interfaces;
    using BookStore.Shared.Resources;

    /// <inheritdoc/>
    public class OutputEnvironment : IOutputEnvironment
    {
        /// <inheritdoc/>
        public string Read()
        {
            return Console.ReadLine();
        }

        /// <inheritdoc/>
        public void ReadKey()
        {
            Console.ReadKey();
        }

        /// <inheritdoc/>
        public int ReadInt(int min, int max)
        {
            var value = ReadInt();

            while (value < min || value > max)
            {
                Console.Write($"{Resource.EnterInteger} {min}-{max}: ");
                value = ReadInt();
            }

            return value;
        }

        /// <inheritdoc/>
        public int ReadInt()
        {
            var input = Console.ReadLine();
            int value;

            while (!int.TryParse(input, out value))
            {
                Console.Write($"{Resource.EnterInteger}: ");
                input = Console.ReadLine();
            }

            return value;
        }

        /// <inheritdoc/>
        public void Write(string str)
        {
            Console.Write(str);
        }

        /// <inheritdoc/>
        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            Console.Clear();
        }
    }
}
