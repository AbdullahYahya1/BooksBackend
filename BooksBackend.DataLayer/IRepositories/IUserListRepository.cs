using BooksBackend.DataLayer.Entities;
namespace BooksBackend.DataLayer.IRepositories
{
    public interface IUserListRepository : IRepository<UserList>
    {
        Task<UserList> GetUserListByIdAsync(int Listid);
    }
}
