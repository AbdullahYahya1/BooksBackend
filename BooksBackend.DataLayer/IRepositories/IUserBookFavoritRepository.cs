using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BooksBackend.DataLayer.IRepositories
{
    public interface IUserBookFavoritRepository:IRepository<UserBookFavorit>
    {

        Task<UserBookFavorit> GetByIdAsync(int bookId, int userid);
        Task<IEnumerable<UserBookFavorit>> GetAllByUserIdAsync(int userId);
        Task<bool> RemoveFavorit(int bookId, int userId);
        Task<bool> FavoritCheck(int bookId, int userId);
        Task<List<ActivityDto>> GetFavoriteActivitiesByUserIdsAsync(List<int> userIds);
    }
}
