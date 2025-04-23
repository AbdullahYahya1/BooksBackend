

namespace BooksBackend.DataLayer.Dto.Books
{
    public class ActivityDto
    {
        public string UserName { get; set; }
        public string BookTitle { get; set; }
        public string BookCover { get; set; }
        public string ActivityType { get; set; } // e.g., "Read", "Rated"
        public double? Rating { get; set; } // Only if it's a rating
        public DateTime ActivityDate { get; set; }
    }

}
