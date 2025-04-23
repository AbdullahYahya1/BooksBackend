using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Entities;
using BooksBackend.DataLayer.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BooksBackend.DataLayer.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly BooksDbContext dbContext;

        public BookRepository(BooksDbContext context) : base(context)
        {
            dbContext = context;
        }

        public async Task<Book> GetBookById(int id)
        {
            return await dbContext.Books.Include(B => B.Reviews).Include(B => B.BookGenres).ThenInclude(BG=>BG.Genre).FirstOrDefaultAsync(B => B.BookId == id); 
        }

        public async Task<ICollection<Book>> GetBooksAsync(BookDtoGetQuery query)
        {
            var booksQuery = dbContext.Books
                .Include(b => b.BookGenres)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.Author))
            {
                booksQuery = booksQuery.Where(b => b.Author.Contains(query.Author));
            }

            if (query.GenreId.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.BookGenres.Any(bg => bg.GenreId == query.GenreId.Value));
            }

            if (query.MinRating.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.Reviews.Average(r => r.Rating) >= query.MinRating.Value);
            }

            var skip = (query.PageNumber - 1) * query.PageSize;

            return await booksQuery
                .Skip(skip)
                .Take(query.PageSize)
                .ToListAsync();
        }

    }
}
