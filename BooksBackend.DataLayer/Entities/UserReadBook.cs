using System.ComponentModel.DataAnnotations;

namespace BooksBackend.DataLayer.Entities
{
    public class UserReadBook
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
        public DateTime ReadDate { get; set; } = DateTime.UtcNow;

    }
}
