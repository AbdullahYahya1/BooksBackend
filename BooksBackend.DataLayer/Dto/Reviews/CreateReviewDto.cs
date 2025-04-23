namespace BooksBackend.DataLayer.Dto.Reviews
{
    public class CreateReviewDto
    {
        public int BookId { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
    }
}
