using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Entities;

namespace BooksBackend.DataLayer.IRepositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<List<Review>> GetReviewsByBookIdAsync(int bookId);
        Task<List<ActivityDto>> GetRatingActivitiesByUserIdsAsync(List<int> userIds);
    }
}
