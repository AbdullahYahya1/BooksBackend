using System.ComponentModel.DataAnnotations;
namespace BooksBackend.DataLayer.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public int Pages { get; set; }
        public double Rating { get; set; } = 0; 
        public int ReadCount { get; set; } = 0;
        public int ReviewsCount { get; set; } = 0; 
        public List<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
        public virtual ICollection<UserReadBook> UserReadBooks { get; set; } = new List<UserReadBook>();
        public virtual ICollection<UserList> UserLists { get; set; } = new List<UserList>();

    }
}
