using AutoMapper;
using BooksBackend.DataLayer.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IBookRepository Books { get; }
    IReviewRepository Reviews { get; }
    IUserFollowRepository UserFollows { get; }
    IUserListRepository UserLists { get; }
    IUserReadBookRepository UserReadBooks { get; }
    IGenreRepository Genres { get; }
    IBookGenreRepository BookGenres { get; }

    IMapper Mapper { get; }
    IHttpContextAccessor HttpContextAccessor { get; }
    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    int? GetCurrentUserId();
}
