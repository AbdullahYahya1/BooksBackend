using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.Dto.Users;
using BooksBackend.DataLayer.Entities;
using BooksBackend.DataLayer.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BooksBackend.DataLayer.Repositories
{
    public class UserFollowRepository : Repository<UserFollow>, IUserFollowRepository
    {
        private readonly BooksDbContext dbContext;

        public UserFollowRepository(BooksDbContext context) : base(context)
        {
            dbContext = context;
        }

        public async Task<UserFollow> GetByFollowerIdAndFollowingId(int FollowerId, int FollowingId)
        {
            return await dbContext.userFollows.FirstOrDefaultAsync(U => U.FollowerId == FollowerId && U.FollowingId == FollowingId); 
        }

        public async Task<ICollection<User>> GetFollowersAsync(int userId)
        {
            return await dbContext.userFollows.Where(f => f.FollowingId == userId)
             .Select(f => f.Follower)
             .ToListAsync();

        }

        public async Task<ICollection<User>> GetFollowingAsync(int userId)
        {
            return await dbContext.userFollows.Where(f => f.FollowerId == userId)
            .Select(f => f.Following)
            .ToListAsync();
        }
    }
}
