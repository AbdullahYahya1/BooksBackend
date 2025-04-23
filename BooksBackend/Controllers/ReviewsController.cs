using BooksBackend.BusinessLayer.IService;
using BooksBackend.DataLayer.Dto.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetReviewsForBook(int bookId)
        {
            var response = await _reviewService.GetReviewsForBookAsync(bookId);
            return Ok(response);
        }


        [HttpPost]
        
        [Authorize]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
        {
            var response = await _reviewService.CreateReviewAsync(dto);
            return Ok(response);
        }

        [HttpPut("{reviewId}")]
        [Authorize]
        public async Task<IActionResult> UpdateReview(int reviewId, [FromBody] UpdateReviewDto dto)
        {
            var response = await _reviewService.UpdateReviewAsync(reviewId, dto);
            return Ok(response);
        }

        [HttpDelete("{reviewId}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var response = await _reviewService.DeleteReviewAsync(reviewId);
            return Ok(response);
        }
    }
}
