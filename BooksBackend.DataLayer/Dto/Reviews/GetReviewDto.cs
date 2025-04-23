

using BooksBackend.DataLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BooksBackend.DataLayer.Dto.Users;

namespace BooksBackend.DataLayer.Dto.Reviews
{
    public class GetReviewDto
    {
        public int ReviewId { get; set; }
        public GetUserDto User { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } 
        public double Rating { get; set; }
    }
}
