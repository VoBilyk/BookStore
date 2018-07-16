using System;
using System.Collections.Generic;


namespace BookStore.ConsoleApp.Menu
{
    public class MenuVisualizer
    {
        private IList<(string Name, Action Callback)> Options { get; set; }

        public MenuVisualizer()
        {
            Options = new List<(string, Action)>();
        }

        public void Display()
        {
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Options[i].Name}");
            }

            Console.Write("Your choice: ");
            int value = ReadInt(1, Options.Count);

            Console.Clear();
            Options[value - 1].Callback();
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
            Options.Add((option, callback));
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
