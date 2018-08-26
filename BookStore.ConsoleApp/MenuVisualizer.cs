namespace BookStore.ConsoleApp
{
    using System;
    using System.Collections.Generic;

    using BookStore.ConsoleApp.Interfaces;

    /// <inheritdoc/>
    public class MenuVisualizer : IMenuVisualizer
    {
        private readonly IOutputEnvironment _outputEnvironment;
        private readonly IList<(string Name, Action Callback)> _options;

        public MenuVisualizer(IOutputEnvironment outputEnvironment)
        {
            this._outputEnvironment = outputEnvironment;
            _options = new List<(string, Action)>();
        }

        /// <inheritdoc/>
        public void ShowCollection<T>(IList<T> items)
        {
            for (var i = 0; i < items.Count; i++)
            {
                _outputEnvironment.WriteLine($"{i + 1}. {items[i]}");
            }
        }

        /// <inheritdoc/>
        public void Display()
        {
            for (var i = 0; i < _options.Count; i++)
            {
                _outputEnvironment.WriteLine($"{i + 1}. {_options[i].Name}");
            }

            _outputEnvironment.Write("Your choice: ");
            var value = _outputEnvironment.ReadInt(1, _options.Count);

            _outputEnvironment.Clear();
            _options[value - 1].Callback();
        }

        /// <inheritdoc/>
        public IMenuVisualizer Add(string option, Action callback)
        {
            _options.Add((option, callback));
            return this;
        }

        /// <inheritdoc/>
        public IMenuVisualizer FactoryMethod()
        {
            return new MenuVisualizer(_outputEnvironment);
        }
    }
}
