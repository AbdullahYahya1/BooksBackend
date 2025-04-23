using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Entities;
using BooksBackend.DataLayer.IRepositories;
using Microsoft.EntityFrameworkCore;

public class UserReadBookRepository : Repository<UserReadBook>, IUserReadBookRepository
{
    private readonly BooksDbContext dbContext;

    public UserReadBookRepository(BooksDbContext context) : base(context)
    {
        dbContext = context;
    }

    public async Task<List<ActivityDto>> GetReadActivitiesByUserIdsAsync(List<int> userIds)
    {
        return await dbContext.userReadBooks
            .Where(r => userIds.Contains(r.UserId))
            .OrderByDescending(r => r.ReadDate) 
            .Select(r => new ActivityDto
            {
                UserName = r.User.Username,
                BookTitle = r.Book.Title,
                BookCover = r.Book.CoverImageUrl,
                ActivityType = "Read",
                ActivityDate = r.ReadDate
            })
            .Take(10)
            .ToListAsync();
    }
}
