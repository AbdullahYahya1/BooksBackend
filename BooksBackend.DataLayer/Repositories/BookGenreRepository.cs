using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.Entities;
using BooksBackend.DataLayer.IRepositories;

namespace BooksBackend.DataLayer.Repositories
{
    public class BookGenreRepository : Repository<BookGenre>, IBookGenreRepository
    {
        private readonly BooksDbContext dbContext;

        public BookGenreRepository(BooksDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
