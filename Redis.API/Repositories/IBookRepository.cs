using Redis.API.Entities;

namespace Redis.API.Repositories
{
    public interface IBookRepository
    {
        Task<IReadOnlyList<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int bookId);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int bookId);
    }
}
