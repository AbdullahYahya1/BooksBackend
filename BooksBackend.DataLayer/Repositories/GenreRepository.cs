using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.Entities;
using BooksBackend.DataLayer.IRepositories;

namespace BooksBackend.DataLayer.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly BooksDbContext dbContext;

        public GenreRepository(BooksDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
