
namespace BooksBackend.DataLayer.Dto.Books
{
    public class BookWithGenresDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public int Pages { get; set; }
        public double Rating { get; set; }
        public int ReviewsCount { get; set; }

        public List<string> Genres { get; set; }
    }
}
