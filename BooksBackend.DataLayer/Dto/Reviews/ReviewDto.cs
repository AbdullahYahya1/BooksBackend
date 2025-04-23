namespace BooksBackend.DataLayer.Dto.Reviews
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
    }
}
