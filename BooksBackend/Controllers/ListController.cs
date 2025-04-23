using BooksBackend.BusinessLayer.IService;
using BooksBackend.DataLayer.Dto.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;

        public ListController(IListService listService)
        {
            _listService = listService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddList([FromBody] CreateListDto dto)
        {
            var response = await _listService.AddListAsync(dto);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{listId}")]
        public async Task<IActionResult> RemoveList(int listId)
        {
            var response = await _listService.RemoveListAsync(listId);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("{listId}/books/{bookId}")]
        public async Task<IActionResult> AddBookToList(int listId, int bookId)
        {
            var response = await _listService.AddBookToListAsync(listId, bookId);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{listId}/books/{bookId}")]
        public async Task<IActionResult> RemoveBookFromList(int listId, int bookId)
        {
            var response = await _listService.RemoveBookFromListAsync(listId, bookId);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetLists()
        {
            var response = await _listService.GetListsAsync();
            return Ok(response);
        }

        [HttpGet("{listId}")]
        public async Task<IActionResult> GetList(int listId)
        {
            var response = await _listService.GetListByIdAsync(listId);
            return Ok(response);
        }
    }
}
