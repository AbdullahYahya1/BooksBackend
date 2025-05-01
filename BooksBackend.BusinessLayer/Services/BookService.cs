using BooksBackend.BusinessLayer.IService;
using BooksBackend.DataLayer.Dto.BookGenre;
using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Dto.General;
using BooksBackend.DataLayer.Entities;


namespace BooksBackend.BusinessLayer.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel<GetBookDto>> CreateBookAsync(CreateOrUpdateBookDto bookDto)
        {
            var book = _unitOfWork.Mapper.Map<Book>(bookDto);
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveChangesAsync(); 
            var bookGenres = bookDto.GenreIds
                .Select(genreId => new BookGenre { BookId = book.BookId, GenreId = genreId })
                .ToList();
            book.BookGenres = bookGenres;

            if (!string.IsNullOrEmpty(bookDto.CoverImageBased64bit))
            {
                var imageBytes = Convert.FromBase64String(bookDto.CoverImageBased64bit);
                var uniqueFileName = $"{Guid.NewGuid()}.jpg";
                var physicalPath = Path.Combine("wwwroot", "images", uniqueFileName);
                await File.WriteAllBytesAsync(physicalPath, imageBytes);
                var relativeImagePath = Path.Combine("images", uniqueFileName).Replace("\\", "/");
                book.CoverImageUrl = relativeImagePath;
            }
            await _unitOfWork.SaveChangesAsync();
            var resultDto = _unitOfWork.Mapper.Map<GetBookDto>(book);
            return new ResponseModel<GetBookDto>
            {
                Result = resultDto,
                IsSuccess = true,
            };
        }


        public async Task<ResponseModel<GetBookDtoByID>> GetBookByIdAsync(int bookId)
        {
            var book = await _unitOfWork.Books.GetBookById(bookId);
            if (book is null)
            {
                return new ResponseModel<GetBookDtoByID>
                {
                    Message = "NotFound",
                    IsSuccess = false,
                };
            }
            var bookDto = _unitOfWork.Mapper.Map<GetBookDtoByID>(book);

            var CurrentUserId = _unitOfWork.GetCurrentUserId();
            if (CurrentUserId != null)
            {
                var RatingCheck = await _unitOfWork.Reviews.ReviewCheck(bookId, (int)CurrentUserId);
                var ReadCheck = await _unitOfWork.UserReadBooks.ReadCheck(bookId, (int)CurrentUserId);
                var FavoritCheck = await _unitOfWork.UserBookFavorits.FavoritCheck(bookId, (int)CurrentUserId);
                bookDto.isRated = RatingCheck;
                bookDto.isRead = ReadCheck;
                bookDto.isFavorit = FavoritCheck;
            }


            return new ResponseModel<GetBookDtoByID>
            {
                Result = bookDto,
                IsSuccess = true,
            };
        }

        public async Task<ResponseModel<List<GetBookDto>>> GetBooksAsync(BookDtoGetQuery query)
        {
            var books = await _unitOfWork.Books.GetBooksAsync(query);
            return new ResponseModel<List<GetBookDto>> { IsSuccess = true, Result = _unitOfWork.Mapper.Map<List<GetBookDto>>(books) }; 
        }

        public async Task<ResponseModel> MarkAsReadAsync(int bookId)
        {
            var existingBook = await _unitOfWork.Books.GetByIdAsync(bookId);

            if (existingBook == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Book not found."
                };
            }

            var userId = _unitOfWork.GetCurrentUserId();
            if(userId  == null)
            {
                return new ResponseModel
                {
                    Message = "NoAuth",
                    IsSuccess = false,
                };
            }
            var alreadyRead = await _unitOfWork.UserReadBooks.FindAsync(urb => urb.BookId == bookId && urb.UserId == (int)userId);

            if (alreadyRead != null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Book already marked as read."
                };
            }
            existingBook.ReadCount += 1;

            await _unitOfWork.Books.UpdateAsync(existingBook);  
            await _unitOfWork.UserReadBooks.AddAsync(new UserReadBook() { BookId = bookId, UserId = (int)userId });
            await _unitOfWork.SaveChangesAsync();
            return new ResponseModel
            {
                IsSuccess = true
            }; 

        }

        public async Task<ResponseModel<GetBookDto>> UpdateBookAsync(int bookId, CreateOrUpdateBookDto bookDto)
        {
            var existingBook = await _unitOfWork.Books.GetByIdAsync(bookId);

            if (existingBook == null)
            {
                return new ResponseModel<GetBookDto>
                {
                    IsSuccess = false,
                    Message = "Book not found."
                };
            }
            _unitOfWork.Mapper.Map(bookDto, existingBook);
            existingBook.BookGenres.Clear();
            var updatedGenres = bookDto.GenreIds
                .Select(id => new BookGenre { BookId = bookId, GenreId = id })
                .ToList();
            existingBook.BookGenres = updatedGenres;
            if (!string.IsNullOrEmpty(bookDto.CoverImageBased64bit))
            {
                var imageBytes = Convert.FromBase64String(bookDto.CoverImageBased64bit);
                var uniqueFileName = $"{Guid.NewGuid()}.jpg";
                var physicalPath = Path.Combine("wwwroot", "images", uniqueFileName);
                await File.WriteAllBytesAsync(physicalPath, imageBytes);
                var relativeImagePath = Path.Combine("images", uniqueFileName).Replace("\\", "/");
                existingBook.CoverImageUrl = relativeImagePath;
            }
            await _unitOfWork.Books.UpdateAsync(existingBook);
            await _unitOfWork.SaveChangesAsync();

            var resultDto = _unitOfWork.Mapper.Map<GetBookDto>(existingBook);

            return new ResponseModel<GetBookDto>
            {
                Result = resultDto,
                IsSuccess = true,
            };
        }

        public async Task<ResponseModel<List<GenreDto>>> GetAllGenresAsync()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();
            var genreDtos = _unitOfWork.Mapper.Map<List<GenreDto>>(genres);

            return new ResponseModel<List<GenreDto>>
            {
                Result = genreDtos,
                IsSuccess = true
            };
        }

        public async Task<ResponseModel> CreateGenreAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Genre name cannot be empty."
                };
            }

            var genre = new Genre { Name = name };
            await _unitOfWork.Genres.AddAsync(genre);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Genre created successfully."
            };
        }

        public async Task<ResponseModel> DeleteGenreAsync(int genreId)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
            if (genre == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Genre not found."
                };
            }

            await _unitOfWork.Genres.DeleteAsync(genreId);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Genre deleted successfully."
            };
        }

        public async Task<ResponseModel> RemoveFromFavoritesAsync(int bookId)
        {
            var userid = _unitOfWork.GetCurrentUserId();
            if (userid == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "NoAuth"
                };
            }
            var userBookFavorit =await _unitOfWork.UserBookFavorits.GetByIdAsync(bookId, (int)userid);
            if (userBookFavorit == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Book not found in favorites."
                };
            }
            await _unitOfWork.UserBookFavorits.RemoveFavorit(bookId, (int)userid); 
            await _unitOfWork.SaveChangesAsync();
            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Book removed from favorites."
            };

        }

        public async Task<ResponseModel> AddToFavoritesAsync(int bookId)
        {
            var userid = _unitOfWork.GetCurrentUserId();

            var userBookFavoritCheck = await _unitOfWork.UserBookFavorits.GetByIdAsync(bookId, (int)userid);

            if (userBookFavoritCheck != null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Book alrady added to favorites."

                };
            }

            if (userid == null)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "NoAuth"
                };
            }
            var userBookFavorit = new UserBookFavorit { BookId = bookId, UserID = (int)userid };
            await _unitOfWork.UserBookFavorits.AddAsync(userBookFavorit);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseModel
            {
                IsSuccess = true,
                Message = "Book added to favorites."
            };
        }

        public async Task<ResponseModel<List<GetBookDto>>> GetFavoriteBooksAsync()
        {
            var userId = _unitOfWork.GetCurrentUserId();
            if (userId == null)
            {
                return new ResponseModel<List<GetBookDto>>
                {
                    IsSuccess = false,
                    Message = "NoAuth"
                };
            }
            var favoriteBooks =await _unitOfWork.UserBookFavorits.GetAllByUserIdAsync((int)userId);
            var result = _unitOfWork.Mapper.Map<List<GetBookDto>>(favoriteBooks.Select(B=>B.Book).ToList());
            return new ResponseModel<List<GetBookDto>>
            {
                IsSuccess = true,
                Result = result
            };
        }

        public async Task<ResponseModel<List<GetBookDto>>> GetMostPopularBooks()
        {
            var books = await _unitOfWork.Books.GetMostPopularBooksAsync();

            var result = _unitOfWork.Mapper.Map<List<GetBookDto>>(books);
            return new ResponseModel<List<GetBookDto>>
            {
                IsSuccess = true,
                Result = result
            };
        }
    }
}
