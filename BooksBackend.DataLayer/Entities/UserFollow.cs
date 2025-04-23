using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksBackend.DataLayer.Entities
{
    public class UserFollow
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Follower")]
        public int FollowerId { get; set; }
        public User Follower { get; set; }

        [ForeignKey("Following")]
        public int FollowingId { get; set; }
        public User Following { get; set; }

        public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
    }
}
