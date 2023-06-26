using Redis.API.Entities;

namespace Redis.API.Services
{
    public interface IBookService
    {
        Task<(IReadOnlyList<Book>, bool)> GetBooksAsync();
        Task<(Book, bool)> GetBookAsync(int bookId);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
