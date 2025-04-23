using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.Entities;
using BooksBackend.DataLayer.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BooksBackend.DataLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly BooksDbContext dbContext;

        public UserRepository(BooksDbContext context) : base(context)
        {
            dbContext = context;
        }
        public async Task<User> FindByRefreshTokenAsync(string refreshToken)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}
