

namespace BooksBackend.DataLayer.Dto.Books
{
    public class CreateOrUpdateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Description { get; set; }
        public string CoverImageBased64bit{ get; set; }
        public int Pages { get; set; }
        public List<int> GenreIds { get; set; }
    }
}
