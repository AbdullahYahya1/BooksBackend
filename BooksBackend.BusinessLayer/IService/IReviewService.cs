using BooksBackend.DataLayer.Dto.Reviews;
using BooksBackend.DataLayer.Dto.General;

namespace BooksBackend.BusinessLayer.IService
{
    public interface IReviewService
    {
        Task<ResponseModel<List<GetReviewDto>>> GetReviewsForBookAsync(int bookId);
        Task<ResponseModel<GetReviewDto>> CreateReviewAsync(CreateReviewDto dto);
        Task<ResponseModel<GetReviewDto>> UpdateReviewAsync(int reviewId, UpdateReviewDto dto);
        Task<ResponseModel<string>> DeleteReviewAsync(int reviewId);
    }
}
