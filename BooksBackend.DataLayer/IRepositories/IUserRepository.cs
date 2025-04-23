using BooksBackend.DataLayer.Entities;


namespace BooksBackend.DataLayer.IRepositories
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> FindByRefreshTokenAsync(string refreshToken);

    }
}
