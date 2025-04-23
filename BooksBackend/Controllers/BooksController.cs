using BooksBackend.BusinessLayer.IService;
using BooksBackend.DataLayer.Dto.BookGenre;
using BooksBackend.DataLayer.Dto.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] BookDtoGetQuery query)
        {
            var response = await _bookService.GetBooksAsync(query);
            return Ok(response);
        }

        [HttpGet("{bookId}")]

        public async Task<IActionResult> GetBook(int bookId)
        {
            var response = await _bookService.GetBookByIdAsync(bookId);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateBook([FromBody] CreateOrUpdateBookDto dto)
        {
            var response = await _bookService.CreateBookAsync(dto);
            return Ok(response);
        }

        [HttpPut("{bookId}")]
        [Authorize]

        public async Task<IActionResult> UpdateBook(int bookId, [FromBody] CreateOrUpdateBookDto dto)
        {
            var response = await _bookService.UpdateBookAsync(bookId, dto);
            return Ok(response);
        }

        
        [HttpPost("{bookId}/mark-read")]
        [Authorize]

        public async Task<IActionResult> MarkBookAsRead(int bookId)
        {
            var response = await _bookService.MarkAsReadAsync(bookId);
            return Ok(response);
        }

        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            var response = await _bookService.GetAllGenresAsync();
            return Ok(response);
        }

        [HttpPost("genres")]
        public async Task<IActionResult> CreateGenre([FromBody] PostGenerDto postGenerDto)
        {
            var response = await _bookService.CreateGenreAsync(postGenerDto.generName);
            return Ok(response);
        }

        [HttpDelete("genres/{genreId}")]
        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            var response = await _bookService.DeleteGenreAsync(genreId);
            return Ok(response);
        }
    }
}