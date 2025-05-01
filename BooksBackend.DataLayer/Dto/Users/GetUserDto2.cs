using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksBackend.DataLayer.Dto.Users
{
    public class GetUserDto2
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string Email { get; set; }
    }
}
