using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.Entities;
using BooksBackend.DataLayer.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BooksBackend.DataLayer.Repositories
{
    public class UserListRepository : Repository<UserList>, IUserListRepository
    {
        private readonly BooksDbContext dbContext;

        public UserListRepository(BooksDbContext context) : base(context)
        {
            dbContext = context;
        }

        public async Task<UserList> GetUserListByIdAsync(int Listid)
        {
            return await dbContext.userLists.Include(L=>L.Books).FirstOrDefaultAsync(L=>L.Id == Listid);  

        }
    }
}
