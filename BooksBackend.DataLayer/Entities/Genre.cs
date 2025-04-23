using System.ComponentModel.DataAnnotations;

namespace BooksBackend.DataLayer.Entities
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
