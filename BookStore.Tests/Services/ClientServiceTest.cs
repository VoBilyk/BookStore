namespace BookStore.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using FakeItEasy;
    using FluentValidation;
    using FluentValidation.Results;
    using NUnit.Framework;

    using BookStore.BLL.MappingProfiles;
    using BookStore.BLL.Services;
    using BookStore.BLL.Validators;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;
    using BookStore.Shared.DTOs;

    [TestFixture]
    public class ClientServiceTest
    {
        private IUnitOfWork _unitOfWorkFake;
        private IMapper _mapperFake;
        private IMapper _mapper;
        private IValidator<Client> _validator;
        private IValidator<Client> _alwaysValidValidator;

        private ClientDto clientDto;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            this._mapper = config.CreateMapper();
            this._unitOfWorkFake = A.Fake<IUnitOfWork>();
            this._mapperFake = A.Fake<IMapper>();
            this._validator = new ClientValidator();

            this._alwaysValidValidator = A.Fake<IValidator<Client>>();
            A.CallTo(() => _alwaysValidValidator.Validate(A<Client>._)).Returns(new ValidationResult());

            this.clientDto = new ClientDto
            {
                Id = Guid.NewGuid(),
                BirthDate = DateTime.Now,
                FirstName = "FName",
                LastName = "LName",
                CommentsId = new List<Guid> { Guid.NewGuid() },
                WishedBooksId = new List<Guid> { Guid.NewGuid() }
            };
        }

        [Test]
        public void GetAll_WhenRequestAllClients_ThenReturnAllSavedClients()
        {
            // Arrange
            var clients = new List<Client>() { new Client { }, new Client { } };

            A.CallTo(() => _unitOfWorkFake.ClientRepository.Get()).Returns(clients);

            A.CallTo(() => _unitOfWorkFake.WishListRepository.Find(A<Func<Wish, bool>>._))
                .Returns(new List<Wish> { new Wish() });

            A.CallTo(() => _unitOfWorkFake.CommentRepository.Find(A<Func<Comment, bool>>._))
                .Returns(new List<Comment> { new Comment() });

            var service = new ClientService(_unitOfWorkFake, _mapper, _alwaysValidValidator);

            // Act
            var returnedClients = service.GetAll();

            // Assert
            Assert.AreEqual(returnedClients.Count, clients.Count);
        }

        [Test]
        public void Get_WhenIdIsPassed_ThenReturnClientWithThisId()
        {
            // Arrange
            var clientId = Guid.NewGuid();

            A.CallTo(() => _unitOfWorkFake.ClientRepository.Get(clientId))
                .Returns(new Client { Id = clientId });

            A.CallTo(() => _unitOfWorkFake.WishListRepository.Find(A<Func<Wish, bool>>._))
                .Returns(new List<Wish> { new Wish() });

            A.CallTo(() => _unitOfWorkFake.CommentRepository.Find(A<Func<Comment, bool>>._))
                .Returns(new List<Comment> { new Comment() });

            var service = new ClientService(_unitOfWorkFake, _mapper, _alwaysValidValidator);

            // Act
            var returnedClient = service.Get(clientId);

            // Assert
            Assert.AreEqual(returnedClient.Id, clientId);
        }

        public void Find_WhenFindClient_ThenReturnNeeded()
        {
            // Arrange
            var client = new Client { FirstName = "FName" };

            var service = new ClientService(_unitOfWorkFake, _mapper, _validator);
            A.CallTo(() => _unitOfWorkFake.ClientRepository.Find(A<Func<Client, bool>>._))
                .Returns(new List<Client> { client });

            // Act
            var returnedClient = service.Find(clientDto);

            // Assert
            Assert.AreEqual(clientDto.FirstName, returnedClient.FirstName);
        }

        [Test]
        public void Find_WhenFindUnknownCLient_ThenThrowArgumentException()
        {
            // Arrange
            var client = new ClientDto();

            var service = new ClientService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<ArgumentException>(() => service.Find(client));
        }

        [Test]
        public void Create_WhenClientIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var client = new ClientDto();

            var service = new ClientService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<ValidationException>(() => service.Create(client));
        }

        [Test]
        public void Update_WhenClientIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var client = new ClientDto();
            var clientId = Guid.NewGuid();

            var service = new ClientService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<ValidationException>(() => service.Update(clientId, client));
        }

        [Test]
        public void Update_WhenClientUpdate_ThenInvokeUpdateByRepository()
        {
            // Arrange
            var service = new ClientService(_unitOfWorkFake, _mapper, _validator);

            // Act
            service.Update(clientDto.Id, clientDto);

            // Assert
            A.CallTo(() => _unitOfWorkFake.ClientRepository.Update(A<Client>._)).MustHaveHappened();
            A.CallTo(() => _unitOfWorkFake.WishListRepository.Create(A<Wish>._)).MustHaveHappened();
        }

        [Test]
        public void Delete_WhenDeleteClient_ThenInvokeDeleteByRepository()
        {
            // Arrange
            var clientId = Guid.NewGuid();

            var service = new ClientService(_unitOfWorkFake, _mapper, _validator);
            service.Delete(clientId);

            // Act - Assert
            A.CallTo(() => _unitOfWorkFake.ClientRepository.Delete(clientId)).MustHaveHappened();
        }

        [Test]
        public void Create_WhenGetClient_ThenInvokedMapper()
        {
            // Arrange
            var client = new Client { FirstName = "FName", LastName = "LName"};

            A.CallTo(() => _mapperFake.Map<ClientDto, Client>(clientDto)).Returns(client);
            var service = new ClientService(_unitOfWorkFake, _mapperFake, _validator);
            service.Create(clientDto);

            // Act - Assert
            A.CallTo(() => _mapperFake.Map<ClientDto, Client>(clientDto)).MustHaveHappened();
        }
    }
}