namespace BookStore.BLL.Services
{
    using System.IO;

    using BookStore.BLL.Interfaces;
    using BookStore.DAL.Interfaces;
    using BookStore.DAL.Models;

    /// <inheritdoc/>
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileSerializer _defaultFileSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="uow">UnitOfWork implementation</param>
        /// <param name="serializer">File serializer implementation</param>
        public FileService(
            IUnitOfWork uow,
            IFileSerializer serializer)
        {
            this._uow = uow;
            this._defaultFileSerializer = serializer;
        }

        /// <inheritdoc/>
        public void ReadFromFile(string fileName, IFileSerializer serializer = null)
        {
            var fileSerializer = serializer ?? _defaultFileSerializer;

            var folderName = "storage";
            Directory.CreateDirectory(folderName);

            var books = fileSerializer.Read<Book>($"{folderName}/{fileName}_books.{fileSerializer.Name}");
            _uow.BookRepository.CreateMany(books);

            var clients = fileSerializer.Read<Client>($"{folderName}/{fileName}_client.{fileSerializer.Name}");
            _uow.ClientRepository.CreateMany(clients);

            var comments = fileSerializer.Read<Comment>($"{folderName}/{fileName}_comment.{fileSerializer.Name}");
            _uow.CommentRepository.CreateMany(comments);

            var wishList = fileSerializer.Read<Wish>($"{folderName}/{fileName}_wishList.{fileSerializer.Name}");
            _uow.WishListRepository.CreateMany(wishList);
        }

        /// <inheritdoc/>
        public void SaveToFile(string fileName, IFileSerializer serializer = null)
        {
            var fileSerializer = serializer ?? _defaultFileSerializer;

            var folderName = "storage";
            Directory.CreateDirectory(folderName);

            var books = _uow.BookRepository.Get();
            fileSerializer.Write(books, $"{folderName}/{fileName}_books.{fileSerializer.Name}");

            var clients = _uow.ClientRepository.Get();
            fileSerializer.Write(clients, $"{folderName}/{fileName}_clients.{fileSerializer.Name}");

            var comments = _uow.CommentRepository.Get();
            fileSerializer.Write(comments, $"{folderName}/{fileName}_comments.{fileSerializer.Name}");

            var wishList = _uow.WishListRepository.Get();
            fileSerializer.Write(wishList, $"{folderName}/{fileName}_wishList.{fileSerializer.Name}");
        }
    }
}
