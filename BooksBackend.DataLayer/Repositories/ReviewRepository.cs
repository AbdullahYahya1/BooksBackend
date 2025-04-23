using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Entities;
using BooksBackend.DataLayer.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BooksBackend.DataLayer.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly BooksDbContext dbContext;

        public ReviewRepository(BooksDbContext context) : base(context)
        {
            dbContext = context;
        }

        public async Task<List<ActivityDto>> GetRatingActivitiesByUserIdsAsync(List<int> userIds)
        {
        return await dbContext.Reviews
            .Where(r => userIds.Contains(r.UserId))
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ActivityDto
            {
                UserName = r.User.Username,
                BookTitle = r.Book.Title,
                BookCover = r.Book.CoverImageUrl,
                ActivityType = "Rated",
                Rating = r.Rating,
                ActivityDate = r.CreatedAt
            })
            .Take(10)
            .ToListAsync();
        }

        public async Task<List<Review>> GetReviewsByBookIdAsync(int bookId)
        {
            return await dbContext.Reviews.Include(R => R.User).Where(B=>B.BookId==bookId).ToListAsync();
        }
    }
}
