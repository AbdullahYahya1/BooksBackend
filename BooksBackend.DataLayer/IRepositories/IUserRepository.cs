using BooksBackend.DataLayer.Dto.General;
using BooksBackend.DataLayer.Dto.Users;
using BooksBackend.DataLayer.Entities;


namespace BooksBackend.DataLayer.IRepositories
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> FindByRefreshTokenAsync(string refreshToken);
        Task<ICollection<User>> GetUsersByEmailOrName(string userNameEmail); 
    }
}
