using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Entities;
using System.Net;
namespace BooksBackend.DataLayer.IRepositories
{
    public interface IUserReadBookRepository : IRepository<UserReadBook>
    {
        Task<List<ActivityDto>> GetReadActivitiesByUserIdsAsync(List<int> userIds);

        Task<bool> ReadCheck(int bookId, int currentUserId);

    }
}
