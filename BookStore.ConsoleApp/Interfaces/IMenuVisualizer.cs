﻿namespace BookStore.ConsoleApp.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contract for visualizing menu
    /// </summary>
    public interface IMenuVisualizer
    {
        /// <summary>
        /// Show collection as list
        /// </summary>
        /// <typeparam name="T">Generic type of collection</typeparam>
        /// <param name="items">Collection which need to show</param>
        void ShowCollection<T>(IList<T> items);

        /// <summary>
        /// Display created menu
        /// </summary>
        void Display();

        /// <summary>
        /// Add new menu item
        /// </summary>
        /// <param name="option">Item name</param>
        /// <param name="callback">Operating which need to execute when choose item</param>
        /// <returns>Current IMenuVisualizer instance</returns>
        IMenuVisualizer Add(string option, Action callback);

        /// <summary>
        /// Factory for creating new instance of IMenuVisualizer
        /// </summary>
        /// <returns>New instance of IMenuVisualizer</returns>
        IMenuVisualizer FactoryMethod();
    }
}
