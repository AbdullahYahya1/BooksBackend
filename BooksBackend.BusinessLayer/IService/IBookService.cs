using BooksBackend.DataLayer.Dto.BookGenre;
using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Dto.General;
using BooksBackend.DataLayer.Entities;

namespace BooksBackend.BusinessLayer.IService
{
    public interface IBookService
    {
        Task<ResponseModel<List<GetBookDto>>> GetBooksAsync(BookDtoGetQuery query);
        Task<ResponseModel<GetBookDtoByID>> GetBookByIdAsync(int bookId);
        Task<ResponseModel<GetBookDto>> CreateBookAsync(CreateOrUpdateBookDto bookDto);
        Task<ResponseModel<GetBookDto>> UpdateBookAsync(int bookId, CreateOrUpdateBookDto bookDto);
        Task<ResponseModel> MarkAsReadAsync(int bookId);

        Task<ResponseModel<List<GenreDto>>> GetAllGenresAsync();
        Task<ResponseModel> CreateGenreAsync(string name);
        Task<ResponseModel> DeleteGenreAsync(int genreId);

        Task<ResponseModel> RemoveFromFavoritesAsync(int bookId);
        Task<ResponseModel> AddToFavoritesAsync(int bookId);
        Task<ResponseModel<List<GetBookDto>>> GetFavoriteBooksAsync();

        Task<ResponseModel<List<GetBookDto>>> GetMostPopularBooks();
    }
}
