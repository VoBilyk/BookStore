namespace BookStore.Tests.Services
{
    using AutoMapper;
    using FakeItEasy;
    using FluentValidation;
    using NUnit.Framework;

    using BookStore.BLL.Interfaces;
    using BookStore.BLL.Services;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.Shared.DTOs;

    [TestFixture]
    public class AuthServiceTest
    {
        private IUnitOfWork _unitOfWorkFake;
        private IMapper _mapperFake;
        private IMapper _mapper;
        private IValidator<Book> _validator;
        private IValidator<Book> _alwaysValidValidator;

        private BookDto bookDto;

        [SetUp]
        public void Setup()
        {
            this._unitOfWorkFake = A.Fake<IUnitOfWork>();
        }

        [Test]
        public void Login_WhenLoginByEmptyItem_ThenFalse()
        {
            // Arrange
            var client = new ClientDto();

            var clientServiceFake = A.Fake<IClientService>();
            var service = new AuthService(_unitOfWorkFake, clientServiceFake);

            // Act
            var result = service.Login(client);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Get_WhenGettingNonLoggedClient_ThenReturnNull()
        {
            // Arrange
            var clientServiceFake = A.Fake<IClientService>();
            var service = new AuthService(_unitOfWorkFake, clientServiceFake);

            // Act
            var result = service.GetCurrentClient();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Get_WhenInvokeLogout_ThenReturnTrue()
        {
            // Arrange
            var clientServiceFake = A.Fake<IClientService>();
            var service = new AuthService(_unitOfWorkFake, clientServiceFake);

            // Act
            var result = service.Logout();

            // Assert
            Assert.IsTrue(result);
        }
    }
}