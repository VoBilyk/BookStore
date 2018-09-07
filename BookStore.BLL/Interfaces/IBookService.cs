namespace BookStore.BLL.Interfaces
{
    using BookStore.Shared.DTOs;

    /// <summary>
    /// Contract for book service
    /// </summary>
    public interface IBookService : IService<BookDto>
    {
    }
}
