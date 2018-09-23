using System.Collections;
using System.Collections.Generic;

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
    public class FileServiceTest
    {
        private IUnitOfWork _unitOfWorkFake;
        private IFileSerializer fileSerializer;

        [SetUp]
        public void Setup()
        {
            this._unitOfWorkFake = A.Fake<IUnitOfWork>();
            this.fileSerializer = A.Fake<IFileSerializer>();
        }

        [Test]
        public void Write_WhenWrite_ThenInvokeSaveByRepositoryFalse()
        {
            // Arrange
            var service = new FileService(_unitOfWorkFake, fileSerializer);
            var collection = new List<int>();

            // Act
            service.SaveToFile("");

            // Assert
            A.CallTo(() => fileSerializer.Write(A<IEnumerable<Book>>._, A<string>._)).MustHaveHappened();
        }

        [Test]
        public void Read_WhenRead_ThenInvokeReadByRepositoryFalse()
        {
            // Arrange
            var service = new FileService(_unitOfWorkFake, fileSerializer);

            // Act
            service.ReadFromFile("");

            // Assert
            A.CallTo(() => fileSerializer.Read<Book>(A<string>._)).MustHaveHappened();
        }
    }
}