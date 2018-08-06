namespace BookStore.ConsoleApp.MenuPages
{
    using System;
    using System.Collections.Generic;

    public class MenuVisualizer
    {
        private IList<(string Name, Action Callback)> _options;

        public MenuVisualizer()
        {
            _options = new List<(string, Action)>();
        }

        public void Display()
        {
            for (int i = 0; i < _options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_options[i].Name}");
            }

            Console.Write("Your choice: ");
            int value = ReadInt(1, _options.Count);

            Console.Clear();
            _options[value - 1].Callback();
        }

        public void ShowCollection<T>(IList<T> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");
            }
        }

        public MenuVisualizer Add(string option, Action callback)
        {
            _options.Add((option, callback));
            return this;
        }

        public int ReadInt()
        {
            string input = Console.ReadLine();
            int value;

            while (!int.TryParse(input, out value))
            {
                Console.Write("Please enter an integer: ");
                input = Console.ReadLine();
            }

            return value;
        }

        public int ReadInt(int min, int max)
        {
            int value = ReadInt();

            while (value < min || value > max)
            {
                Console.Write($"Please enter an integer between {min} and {max}: ");
                value = ReadInt();
            }

            return value;
        }
    }
}
