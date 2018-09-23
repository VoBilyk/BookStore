namespace BookStore.Tests.Serializes
{
    using System.Collections.Generic;
    using NUnit.Framework;

    using BookStore.BLL.Interfaces;
    using BookStore.BLL.Serializers;

    [TestFixture]
    public class JsonFileSerializerTest
    {
        [Test]
        public void Write_WhenWrite_ThenNotThrowException()
        {
            // Arrange
            var serializer = new JsonFileSerializer();
            var collection = new List<int>();

            // Act - Assert
            Assert.DoesNotThrow(() => serializer.Write(collection, "file1"));
        }

        [Test]
        public void Read_WhenRead_ThenNotThrowException()
        {
            // Arrange
            var serializer = new JsonFileSerializer();
            var collection = new List<int>();

            // Act - Assert
            Assert.DoesNotThrow(() => serializer.Read<int>("file1"));
        }
    }
}