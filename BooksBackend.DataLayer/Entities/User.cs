using System.ComponentModel.DataAnnotations;

namespace BooksBackend.DataLayer.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public UserType UserType { get; set; }

        public int followers { get; set; } = 0;
        public int following { get; set; } = 0; 
        public virtual ICollection<UserFollow> Followers { get; set; } = new List<UserFollow>(); 
        public virtual ICollection<UserFollow> Following { get; set; } = new List<UserFollow>();
        public virtual ICollection<UserReadBook> UserReadBooks { get; set; } = new List<UserReadBook>();
        public virtual ICollection<UserList> UserLists { get; set; } = new List<UserList>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<UserBookFavorit> UserBookFavorits { get; set; } = new List<UserBookFavorit>();

    }
}
