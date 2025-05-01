using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Entities;
using BooksBackend.DataLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksBackend.DataLayer.Repositories
{
    public class UserBookFavoritRepository: Repository<UserBookFavorit> , IUserBookFavoritRepository
    {
        private readonly BooksDbContext dbContext;
        public UserBookFavoritRepository(BooksDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Task<bool> FavoritCheck(int bookId, int userId)
        {
            return dbContext.UserBookFavorits
                .AnyAsync(x => x.BookId == bookId && x.UserID == userId);
        }

        public async Task<IEnumerable<UserBookFavorit>> GetAllByUserIdAsync(int userId)
        {
            return await dbContext.UserBookFavorits
                .Where(x => x.UserID == userId)
                .Include(x => x.Book)
                .ToListAsync();
        }

        public async Task<UserBookFavorit> GetByIdAsync(int bookId, int userid)
        {
            return await dbContext.UserBookFavorits
                .Where(x => x.BookId == bookId && x.UserID == userid)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveFavorit(int bookId, int userId)
        {
            var favorit = await dbContext.UserBookFavorits
                .Where(x => x.BookId == bookId && x.UserID == userId)
                .FirstOrDefaultAsync();
            if (favorit != null)
            {
                dbContext.UserBookFavorits.Remove(favorit);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<List<ActivityDto>> GetFavoriteActivitiesByUserIdsAsync(List<int> userIds)
        {
            return await dbContext.UserBookFavorits
                .Where(f => userIds.Contains(f.UserID))
                .OrderByDescending(f => f.Id) 
                .Select(f => new ActivityDto
                {
                    UserName = f.User.Username,
                    BookTitle = f.Book.Title,
                    BookCover = f.Book.CoverImageUrl,
                    ActivityType = "Favorited",
                    ActivityDate = f.CreatedAt,
                    bookId = f.BookId,
                })
                .Take(10)
                .ToListAsync();
        }

    }
}
