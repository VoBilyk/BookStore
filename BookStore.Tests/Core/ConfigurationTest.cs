namespace BookStore.Tests.Core
{
    using FakeItEasy;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    using BookStore.ConsoleApp;

    [TestFixture]
    public class ConfigurationTest
    {
        [Test]
        public void Configurate_WhenCongigurate_ThenNotThrowException()
        {
            // Arrange
            var service = A.Fake<IServiceCollection>();

            // Act
            Startup.ConfigureServices(service);

            // Act - Assert
            Assert.DoesNotThrow(() => Startup.ConfigureServices(service));
        }
    }
}