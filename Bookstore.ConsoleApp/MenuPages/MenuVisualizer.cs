namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Menu visualizer for console
    /// </summary>
    public class MenuVisualizer
    {
        private IList<(string Name, Action Callback)> _options;

        public MenuVisualizer()
        {
            _options = new List<(string, Action)>();
        }

        /// <summary>
        /// Show collection as list to console
        /// </summary>
        /// <typeparam name="T">Generic type of collection</typeparam>
        /// <param name="items">Collection which need to show</param>
        public static void ShowCollection<T>(IList<T> items)
        {
            for (var i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");
            }
        }

        /// <summary>
        /// Read int form into console
        /// </summary>
        /// <param name="min">Min possible value</param>
        /// <param name="max">Max possible value</param>
        /// <returns>Entered value</returns>
        public static int ReadInt(int min, int max)
        {
            var value = ReadInt();

            while (value < min || value > max)
            {
                Console.Write($"Please enter an integer between {min} and {max}: ");
                value = ReadInt();
            }

            return value;
        }

        /// <summary>
        /// Getting entered value
        /// </summary>
        /// <returns>Entered value</returns>
        public static int ReadInt()
        {
            var input = Console.ReadLine();
            int value;

            while (!int.TryParse(input, out value))
            {
                Console.Write("Please enter an integer: ");
                input = Console.ReadLine();
            }

            return value;
        }

        /// <summary>
        /// Show menu into console
        /// </summary>
        public void Display()
        {
            for (var i = 0; i < _options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_options[i].Name}");
            }

            Console.Write("Your choice: ");
            var value = ReadInt(1, _options.Count);

            Console.Clear();
            _options[value - 1].Callback();
        }

        /// <summary>
        /// Add new menu item
        /// </summary>
        /// <param name="option">Item name</param>
        /// <param name="callback">Operating which need to execute when choose item</param>
        /// <returns>Current Menu Visualizer instance</returns>
        public MenuVisualizer Add(string option, Action callback)
        {
            _options.Add((option, callback));
            return this;
        }
    }
}
