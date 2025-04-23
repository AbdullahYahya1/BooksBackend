using BooksBackend.DataLayer.Dto.Users;
using BooksBackend.DataLayer.Dto.General;
using BooksBackend.DataLayer.Dto.Books;

namespace BooksBackend.BusinessLayer.IService
{
    public interface IFollowService
    {
        Task<ResponseModel> FollowUserAsync(int userId);
        Task<ResponseModel> UnfollowUserAsync(int  userId);
        Task<ResponseModel<List<GetUserDto>>> GetFollowersAsync(int userId);
        Task<ResponseModel<List<GetUserDto>>> GetFollowingAsync(int userId);
        Task<ResponseModel<List<ActivityDto>>> GetFollowingActivityAsync();
    }
}
