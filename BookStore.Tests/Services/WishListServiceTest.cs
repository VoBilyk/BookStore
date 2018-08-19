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
    public class WishListServiceTest
    {
        private IUnitOfWork _unitOfWorkFake;
        private IMapper _mapper;
        private IValidator<Wish> _validator;
        private IValidator<Wish> _alwaysValidValidator;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            this._mapper = config.CreateMapper();
            this._unitOfWorkFake = A.Fake<IUnitOfWork>();
            this._validator = new WishValidator();

            this._alwaysValidValidator = A.Fake<IValidator<Wish>>();
            A.CallTo(() => _alwaysValidValidator.Validate(A<Wish>._)).Returns(new ValidationResult());
        }

        [Test]
        public void GetAll_WhenRequestAllWishes_ThenReturnAllSavedWishes()
        {
            // Arrange
            var wishLists = new List<Wish>() { new Wish { }, new Wish { } };

            A.CallTo(() => _unitOfWorkFake.WishListRepository.Get()).Returns(wishLists);

            var service = new WishListService(_unitOfWorkFake, _mapper, _alwaysValidValidator);

            // Act
            var returnedWishLists = service.GetAll();

            // Assert
            Assert.AreEqual(returnedWishLists.Count, wishLists.Count);
        }

        [Test]
        public void Get_WhenIdIsPassed_ThenReturnWishWithThisId()
        {
            // Arrange
            var wishId = Guid.NewGuid();

            A.CallTo(() => _unitOfWorkFake.WishListRepository.Get(wishId))
                .Returns(new Wish { Id = wishId });

            var service = new WishListService(_unitOfWorkFake, _mapper, _alwaysValidValidator);

            // Act
            var returnedWishList = service.Get(wishId);

            // Assert
            Assert.AreEqual(returnedWishList.Id, wishId);
        }

        [Test]
        public void Find_WhenFindWish_ThenReturnNeeded()
        {
            // Arrange
            var wishDto = new WishDto();
            var wish = new Wish();

            var service = new WishListService(_unitOfWorkFake, _mapper, _validator);
            A.CallTo(() => _unitOfWorkFake.WishListRepository.Find(A<Func<Wish, bool>>._))
                .Returns(new List<Wish> { wish });

            // Act
            var returnedWish = service.Find(wishDto);

            // Assert
            Assert.AreEqual(wishDto.Id, returnedWish.Id);
        }

        [Test]
        public void Find_WhenFindUnknownWish_ThenThrowArgumentException()
        {
            // Arrange
            var wish = new WishDto();

            var service = new WishListService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<ArgumentException>(() => service.Find(wish));
        }

        [Test]
        public void Create_WhenWishIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var wish = new WishDto();

            var service = new WishListService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<ValidationException>(() => service.Create(wish));
        }

        [Test]
        public void Create_WhenCreateWish_ThenInvokeCreateByRepository()
        {
            // Arrange
            var service = new WishListService(_unitOfWorkFake, _mapper, _alwaysValidValidator);

            // Act
            service.Create(new WishDto());

            // Assert
            A.CallTo(() => _unitOfWorkFake.WishListRepository.Create(A<Wish>._)).MustHaveHappened();
        }

        [Test]
        public void Update_WhenWishIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var wishList = new WishDto();
            var wishListId = Guid.NewGuid();

            var service = new WishListService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<ValidationException>(() => service.Update(wishListId, wishList));
        }

        [Test]
        public void Update_WhenUpdateWish_ThenInvokeUpdateByRepository()
        {
            // Arrange
            var service = new WishListService(_unitOfWorkFake, _mapper, _alwaysValidValidator);

            // Act
            service.Update(Guid.NewGuid(), new WishDto());

            // Assert
            A.CallTo(() => _unitOfWorkFake.WishListRepository.Update(A<Wish>._)).MustHaveHappened();
        }

        [Test]
        public void Delete_WhenDeleteWish_ThenInvokeDeleteByRepository()
        {
            // Arrange
            var wishId = Guid.NewGuid();

            var service = new WishListService(_unitOfWorkFake, _mapper, _validator);
            service.Delete(wishId);

            // Act - Assert
            A.CallTo(() => _unitOfWorkFake.WishListRepository.Delete(wishId)).MustHaveHappened();
        }
    }
}