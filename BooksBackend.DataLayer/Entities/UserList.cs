using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BooksBackend.DataLayer.Entities
{
    public class UserList
    {
        [Key]
        public int Id { get; set; }
        public string ListName { get; set; }


        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();


    }
}
