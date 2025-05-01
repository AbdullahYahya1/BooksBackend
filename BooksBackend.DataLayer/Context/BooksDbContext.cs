using BooksBackend.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace BooksBackend.DataLayer.Context
{
    public  class BooksDbContext:DbContext
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserFollow>()
                .HasOne(uf => uf.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<UserFollow>()
                .HasOne(uf => uf.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<BookGenre>()
    .HasKey(bg => new { bg.BookId, bg.GenreId });

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId);

            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.BookGenres)
                .HasForeignKey(bg => bg.GenreId);


            modelBuilder.Entity<UserBookFavorit>()
                .HasKey(ub => new { ub.BookId, ub.UserID });

            modelBuilder.Entity<UserBookFavorit>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.UserBookFavorits)
                .HasForeignKey(bg => bg.BookId);

            modelBuilder.Entity<UserBookFavorit>()
                .HasOne(bg => bg.User)
                .WithMany(g => g.UserBookFavorits)
                .HasForeignKey(bg => bg.UserID);



        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFollow> userFollows { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserList> userLists { get; set; }
        public DbSet<UserReadBook> userReadBooks { get; set; }
        public DbSet<UserBookFavorit> UserBookFavorits { get; set; }

    }
}
