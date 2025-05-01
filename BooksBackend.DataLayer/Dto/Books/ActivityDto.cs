

namespace BooksBackend.DataLayer.Dto.Books
{
    public class ActivityDto
    {
        public string UserName { get; set; }
        public string BookTitle { get; set; }
        public int bookId { get; set; }
        public string BookCover { get; set; }
        public string ActivityType { get; set; } 
        public double? Rating { get; set; } 
        public DateTime ActivityDate { get; set; }
    }

}
