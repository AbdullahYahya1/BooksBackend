
namespace BooksBackend.DataLayer.Dto.Books
{
    public class BookDtoGetQuery
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? GenreId { get; set; }
        public double? MinRating { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
