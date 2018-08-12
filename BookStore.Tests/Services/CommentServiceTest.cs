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
    public class CommentServiceTest
    {
        private IUnitOfWork _unitOfWorkFake;
        private IMapper _mapper;
        private IValidator<Comment> _validator;
        private IValidator<Comment> _alwaysValidValidator;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            this._mapper = config.CreateMapper();
            this._unitOfWorkFake = A.Fake<IUnitOfWork>();
            this._validator = new CommentValidator();

            this._alwaysValidValidator = A.Fake<IValidator<Comment>>();
            A.CallTo(() => _alwaysValidValidator.Validate(A<Comment>._)).Returns(new ValidationResult());
        }

        [Test]
        public void GetAll_WhenRequestAllComments_ThenReturnAllSavedComments()
        {
            // Arrange
            var comments = new List<Comment>() { new Comment { }, new Comment { } };

            A.CallTo(() => _unitOfWorkFake.CommentRepository.Get()).Returns(comments);

            var service = new CommentService(_unitOfWorkFake, _mapper, _alwaysValidValidator);

            // Act
            var returnedComments = service.GetAll();

            // Assert
            Assert.AreEqual(returnedComments.Count, comments.Count);
        }

        [Test]
        public void Get_WhenIdIsPassed_ThenReturnCommentWithThisId()
        {
            // Arrange
            var commentId = Guid.NewGuid();

            A.CallTo(() => _unitOfWorkFake.CommentRepository.Get(commentId))
                .Returns(new Comment { Id = commentId });

            var service = new CommentService(_unitOfWorkFake, _mapper, _alwaysValidValidator);

            // Act
            var returnedComment = service.Get(commentId);

            // Assert
            Assert.AreEqual(returnedComment.Id, commentId);
        }

        [Test]
        public void Find_WhenFindComment_ThenReturnNeeded()
        {
            // Arrange
            var commentDto = new CommentDto { Text = "Text" };
            var comment = new Comment() { Text = "Text" };

            var service = new CommentService(_unitOfWorkFake, _mapper, _validator);
            A.CallTo(() => _unitOfWorkFake.CommentRepository.Find(A<Func<Comment, bool>>._))
                .Returns(new List<Comment> { comment });

            // Act
            var returnedComment = service.Find(commentDto);

            // Assert
            Assert.AreEqual(commentDto.Text, returnedComment.Text);
        }

        [Test]
        public void Find_WhenFindUnknownComment_ThenThrowInvalidOperationExeption()
        {
            // Arrange
            var comment = new CommentDto();

            var service = new CommentService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<InvalidOperationException>(() => service.Find(comment));
        }

        [Test]
        public void Create_WhenCommentIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var comment = new CommentDto();

            var service = new CommentService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<ValidationException>(() => service.Create(comment));
        }

        [Test]
        public void Update_WhenCommentIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var comment = new CommentDto();
            var commentId = Guid.NewGuid();

            var service = new CommentService(_unitOfWorkFake, _mapper, _validator);

            // Act - Assert
            Assert.Throws<ValidationException>(() => service.Update(commentId, comment));
        }

        [Test]
        public void Delete_WhenDeleteComment_ThenInvokeDeleteByRepository()
        {
            // Arrange
            var commentId = Guid.NewGuid();

            var service = new CommentService(_unitOfWorkFake, _mapper, _validator);
            service.Delete(commentId);

            // Act - Assert
            A.CallTo(() => _unitOfWorkFake.CommentRepository.Delete(commentId)).MustHaveHappened();
        }
    }
}