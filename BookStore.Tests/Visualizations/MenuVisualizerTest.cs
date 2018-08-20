using System.Collections.Generic;
using BookStore.ConsoleApp;

namespace BookStore.Tests.Repositories
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using NUnit.Framework;

    using BookStore.DAL;
    using BookStore.DAL.Models;
    using BookStore.DAL.Repositories;

    [TestFixture]
    public class MenuVisualizerTest
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShowCollection_WhenPassCollection_ThenIterateItWithoutException()
        {
            // Arrange
            var collection = new List<int> { 1, 2, 3 };

            // Act - Assert
            Assert.DoesNotThrow(() => MenuVisualizer.ShowCollection(collection));
        }
    }
}