using BooksBackend.DataLayer.Dto.Auth;
using BooksBackend.DataLayer.Dto.General;
using BooksBackend.DataLayer.Dto.Users;

namespace BooksBackend.BusinessLayer.IService
{
    public interface IUserService
    {
        Task<ResponseModel<GetUserDto>> GetCurrentUser();
        Task<ResponseModel<GetUserDto>> RegisterAsync(string username, string email, string password);
        Task<ResponseModel<TokenResponse>> LoginAsync(string email, string password);
        Task<ResponseModel<TokenResponse>> RefreshTokenAsync(string refreshToken);
    }
}
