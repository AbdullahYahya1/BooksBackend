using AutoMapper;
using BooksBackend.DataLayer.Context;
using BooksBackend.DataLayer.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

public class UnitOfWork : IUnitOfWork
{
    private readonly BooksDbContext _db;
    private readonly IMapper _mapper;

    public IHttpContextAccessor HttpContextAccessor { get; }

    public IUserRepository Users { get; }
    public IBookRepository Books { get; }
    public IReviewRepository Reviews { get; }
    public IUserFollowRepository UserFollows { get; }
    public IUserListRepository UserLists { get; }
    public IUserReadBookRepository UserReadBooks { get; }
    public IGenreRepository Genres { get; }
    public IBookGenreRepository BookGenres { get; }

    public IUserBookFavoritRepository UserBookFavorits { get; }

    public IMapper Mapper => _mapper;

    public UnitOfWork(
        BooksDbContext context,
        IUserRepository userRepository,
        IBookRepository bookRepository,
        IReviewRepository reviewRepository,
        IUserFollowRepository userFollowRepository,
        IUserListRepository userListRepository,
        IUserReadBookRepository userReadBookRepository,
        IGenreRepository genreRepository,
        IBookGenreRepository bookGenreRepository,
        IHttpContextAccessor httpContextAccessor,
        IUserBookFavoritRepository BookFavoritsRepository,
        IMapper mapper)
    {
        _db = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        Users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        Books = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        Reviews = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        UserFollows = userFollowRepository ?? throw new ArgumentNullException(nameof(userFollowRepository));
        UserLists = userListRepository ?? throw new ArgumentNullException(nameof(userListRepository));
        UserReadBooks = userReadBookRepository ?? throw new ArgumentNullException(nameof(userReadBookRepository));
        Genres = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
        BookGenres = bookGenreRepository ?? throw new ArgumentNullException(nameof(bookGenreRepository));
        UserBookFavorits = BookFavoritsRepository ?? throw new ArgumentNullException(nameof(BookFavoritsRepository));
    }

    public async Task<int> SaveChangesAsync() => await _db.SaveChangesAsync();

    public async Task<IDbContextTransaction> BeginTransactionAsync() => await _db.Database.BeginTransactionAsync();

    public int? GetCurrentUserId()
    {
        var httpContext = HttpContextAccessor.HttpContext;
        var userIdClaim = httpContext?.User?.FindFirst("UserID");

        if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
        {
            return null;
        }

        return int.Parse(userIdClaim.Value);
    }

}
