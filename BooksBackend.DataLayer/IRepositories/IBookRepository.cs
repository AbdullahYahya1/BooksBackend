using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Entities;

namespace BooksBackend.DataLayer.IRepositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetBookById(int id);
        Task<ICollection<Book>> GetBooksAsync(BookDtoGetQuery query);
    }
}
