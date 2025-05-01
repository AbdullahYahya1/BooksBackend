using BooksBackend.BusinessLayer.IService;
using BooksBackend.DataLayer.Dto.General;
using BooksBackend.DataLayer.Dto.Reviews;
using BooksBackend.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace BooksBackend.BusinessLayer.Services
{
    public class ReviewService : IReviewService
    {

        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ResponseModel<GetReviewDto>> CreateReviewAsync(CreateReviewDto dto)
        {
            var userId = _unitOfWork.GetCurrentUserId();
            if (userId == null)
            {
                return new ResponseModel<GetReviewDto>
                {
                    IsSuccess = false,
                    Message = "Unauthorized"
                };
            }

            var book = await _unitOfWork.Books.GetBookById(dto.BookId);
            if (book == null)
            {
                return new ResponseModel<GetReviewDto>
                {
                    IsSuccess = false,
                    Message = "Book not found"
                };
            }
            var oldReview= book.Reviews.FirstOrDefault(R => R.UserId == userId);

            if(oldReview !=  null)
            {
                return new ResponseModel<GetReviewDto>
                {
                    IsSuccess = false,
                    Message = "you alrady made a review"
                };
            }

            var review = _unitOfWork.Mapper.Map<Review>(dto);
            review.UserId = userId.Value;

            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveChangesAsync(); 

            var updatedBook = await _unitOfWork.Books.GetBookById(dto.BookId); 
            RecalculateBookStats(updatedBook);

            await _unitOfWork.SaveChangesAsync(); 


            var result = _unitOfWork.Mapper.Map<GetReviewDto>(review);
            return new ResponseModel<GetReviewDto> { IsSuccess = true, Result = result };
        }



        public async Task<ResponseModel<string>> DeleteReviewAsync(int reviewId)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(reviewId);
            if (review == null)
            {
                return new ResponseModel<string>
                {
                    IsSuccess = false,
                    Message = "Review not found"
                };
            }
            if(review.UserId != _unitOfWork.GetCurrentUserId())
            {
                return new ResponseModel<string>
                {
                    IsSuccess = false,
                    Message = "Not Allowed"
                };
            }
            var book = await _unitOfWork.Books.GetBookById(review.BookId);
            if (book != null)
            {
                book.Reviews.Remove(review);
                RecalculateBookStats(book);
            }

            await _unitOfWork.Reviews.DeleteAsync(reviewId);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<string>
            {
                IsSuccess = true,
                Result = "Review deleted successfully"
            };
        }


        public async Task<ResponseModel<List<GetReviewDto>>> GetReviewsForBookAsync(int bookId)
        {
            var reviews = await _unitOfWork.Reviews.GetReviewsByBookIdAsync(bookId);

            var result = _unitOfWork.Mapper.Map<List<GetReviewDto>>(reviews);

            return new ResponseModel<List<GetReviewDto>>
            {
                IsSuccess = true,
                Result = result
            };
        }


        public async Task<ResponseModel<GetReviewDto>> UpdateReviewAsync(int reviewId, UpdateReviewDto dto)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(reviewId);
            if (review == null)
            {
                return new ResponseModel<GetReviewDto>
                {
                    IsSuccess = false,
                    Message = "Review not found"
                };
            }
            if (review.UserId != _unitOfWork.GetCurrentUserId())
            {
                return new ResponseModel<GetReviewDto>
                {
                    IsSuccess = false,
                    Message = "Not Allowed"
                };
            }
            _unitOfWork.Mapper.Map(dto, review); 

            var book = await _unitOfWork.Books.GetByIdAsync(review.BookId);
            if (book != null)
            {
                RecalculateBookStats(book);
            }

            await _unitOfWork.SaveChangesAsync();

            var result = _unitOfWork.Mapper.Map<GetReviewDto>(review);
            return new ResponseModel<GetReviewDto> { IsSuccess = true, Result = result };
        }


        private void RecalculateBookStats(Book book)
        {
            var totalReviews = book.Reviews.Count;

            if (totalReviews == 0)
            {
                book.Rating = 0;
                book.ReviewsCount = 0;
                return;
            }

            book.Rating = book.Reviews.Average(r => r.Rating);
            book.ReviewsCount = totalReviews;
        }


    }
}
