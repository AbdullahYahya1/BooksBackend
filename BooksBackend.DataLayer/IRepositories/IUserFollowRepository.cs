using BooksBackend.DataLayer.Entities;

namespace BooksBackend.DataLayer.IRepositories
{
    public interface IUserFollowRepository : IRepository<UserFollow>
    {
        Task<UserFollow> GetByFollowerIdAndFollowingId(int FollowerId,int FollowingId);
        Task<ICollection<User>> GetFollowingAsync(int userId);
        Task<ICollection<User>> GetFollowersAsync(int userId);

    }
}
