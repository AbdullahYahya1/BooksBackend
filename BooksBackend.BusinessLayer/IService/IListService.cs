using BooksBackend.DataLayer.Dto.Lists;
using BooksBackend.DataLayer.Dto.General;

namespace BooksBackend.BusinessLayer.IService
{
    public interface IListService
    {
        Task<ResponseModel<GetListDto>> AddListAsync(CreateListDto dto);
        Task<ResponseModel<string>> RemoveBookFromListAsync(int listId, int bookId);
        Task<ResponseModel<string>> RemoveListAsync(int listId);
        Task<ResponseModel<GetListDto>> AddBookToListAsync(int listId, int bookId);
        Task<ResponseModel<List<GetListDto>>> GetListsAsync();
        Task<ResponseModel<GetListsWithBooksDto>> GetListByIdAsync(int listId);
    }
}
