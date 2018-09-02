namespace BookStore.Tests.Visualizations
{
    using System.Collections.Generic;
    using FakeItEasy;
    using NUnit.Framework;

    using BookStore.ConsoleApp;
    using BookStore.ConsoleApp.Interfaces;

    [TestFixture]
    public class MenuVisualizerTest
    {
        private IOutputEnvironment _outputEnvironment;
        private MenuVisualizer _menu;

        [SetUp]
        public void Setup()
        {
            _outputEnvironment = A.Fake<IOutputEnvironment>();
            _menu = new MenuVisualizer(_outputEnvironment);
        }

        [Test]
        public void ShowCollection_WhenPassCollection_ThenIterateIt()
        {
            // Arrange
            var collection = new List<int> { 1, 2, 3 };

            // Act
            _menu.ShowCollection(collection);

            // Act - Assert
            A.CallTo(() => _outputEnvironment.WriteLine(A<string>._)).MustHaveHappened();
        }

        [Test]
        public void DisplayMenu_WhenDisplayMenu_ThenInvokeInvokeReadChoice()
        {
            // Arrange
            _menu.Add(" ", () => { });
            A.CallTo(() => _outputEnvironment.ReadInt(A<int>._, A<int>._))
                .Returns(1);

            // Act
            _menu.Display();

            // Act - Assert
            A.CallTo(() => _outputEnvironment.ReadInt(A<int>._, A<int>._)).MustHaveHappened();
        }
    }
}