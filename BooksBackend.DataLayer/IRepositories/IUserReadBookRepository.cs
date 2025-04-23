using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Entities;
namespace BooksBackend.DataLayer.IRepositories
{
    public interface IUserReadBookRepository : IRepository<UserReadBook>
    {
        Task<List<ActivityDto>> GetReadActivitiesByUserIdsAsync(List<int> userIds);

    }
}
