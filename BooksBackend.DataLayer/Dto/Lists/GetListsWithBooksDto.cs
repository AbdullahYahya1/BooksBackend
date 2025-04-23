using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksBackend.DataLayer.Dto.Lists
{
    public class GetListsWithBooksDto
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public virtual ICollection<GetBookDto> Books { get; set; } = new List<GetBookDto>();

    }
}
