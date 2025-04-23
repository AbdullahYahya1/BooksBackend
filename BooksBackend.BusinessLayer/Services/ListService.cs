using BooksBackend.BusinessLayer.IService;
using BooksBackend.DataLayer.Dto.General;
using BooksBackend.DataLayer.Dto.Lists;
using BooksBackend.DataLayer.Entities;

namespace BooksBackend.BusinessLayer.Services
{
    public class ListService : IListService
    {

        private readonly IUnitOfWork _unitOfWork;

        public ListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ResponseModel<GetListDto>> AddListAsync(CreateListDto dto)
        {
            var userId = _unitOfWork.GetCurrentUserId();
            if (userId == null)
            {
                return new ResponseModel<GetListDto>
                {
                    IsSuccess = false,
                    Message = "Unauthorized"
                };
            }

            var list = new UserList
            {
                ListName = dto.ListName,
                UserId = userId.Value
            };

            await _unitOfWork.UserLists.AddAsync(list);
            await _unitOfWork.SaveChangesAsync();

            var result = _unitOfWork.Mapper.Map<GetListDto>(list);

            return new ResponseModel<GetListDto>
            {
                IsSuccess = true,
                Result = result
            };
        }



        public async Task<ResponseModel<GetListDto>> AddBookToListAsync(int listId, int bookId)
        {
            var list = await _unitOfWork.UserLists.GetUserListByIdAsync(listId);

            if (list.UserId != _unitOfWork.GetCurrentUserId())
            {
                return new ResponseModel<GetListDto>
                {
                    IsSuccess = false,
                    Message = "Not Authorized"
                };
            }

            var bookCheck = list.Books.FirstOrDefault(b => b.BookId == bookId);
            if (bookCheck != null)
            {
                return new ResponseModel<GetListDto> { IsSuccess = false, Message = "Book in the list" };
            }

            var book = await _unitOfWork.Books.GetByIdAsync(bookId);

            if (list == null || book == null)
            {
                return new ResponseModel<GetListDto>
                {
                    IsSuccess = false,
                    Message = "List or Book not found"
                };
            }

            if (list.Books.Any(b => b.BookId == bookId))
            {
                return new ResponseModel<GetListDto>
                {
                    IsSuccess = false,
                    Message = "Book already in list"
                };
            }

            list.Books.Add(book);
            await _unitOfWork.SaveChangesAsync();

            var result = _unitOfWork.Mapper.Map<GetListDto>(list);

            return new ResponseModel<GetListDto>
            {
                IsSuccess = true,
                Result = result
            };
        }


        public async Task<ResponseModel<GetListsWithBooksDto>> GetListByIdAsync(int listId)
        {
            var list = await _unitOfWork.UserLists.GetUserListByIdAsync(listId);

            if (list == null)
            {
                return new ResponseModel<GetListsWithBooksDto>
                {
                    IsSuccess = false,
                    Message = "List not found"
                };
            }

            var result = _unitOfWork.Mapper.Map<GetListsWithBooksDto>(list);

            return new ResponseModel<GetListsWithBooksDto>
            {
                IsSuccess = true,
                Result = result
            };
        }


        public async Task<ResponseModel<List<GetListDto>>> GetListsAsync()
        {
            var userId = _unitOfWork.GetCurrentUserId();
            if (userId == null)
            {
                return new ResponseModel<List<GetListDto>>
                {
                    IsSuccess = false,
                    Message = "Unauthorized"
                };
            }

            var lists = await _unitOfWork.UserLists.GetAllAsync();
            var userLists = lists.Where(l => l.UserId == userId).ToList();

            var result = _unitOfWork.Mapper.Map<List<GetListDto>>(userLists);

            return new ResponseModel<List<GetListDto>>
            {
                IsSuccess = true,
                Result = result
            };
        }


        public async Task<ResponseModel<string>> RemoveBookFromListAsync(int listId, int bookId)
        {
            var list = await _unitOfWork.UserLists.GetUserListByIdAsync(listId);
            if (list.UserId != _unitOfWork.GetCurrentUserId())
            {
                return new ResponseModel<string>
                {
                    IsSuccess = false,
                    Message = "Not Authorized"
                };
            }

            if (list == null)
            {
                return new ResponseModel<string> { IsSuccess = false, Message = "List not found" };
            }

            var book = list.Books.FirstOrDefault(b => b.BookId == bookId);
            if (book == null)
            {
                return new ResponseModel<string> { IsSuccess = false, Message = "Book not in list" };
            }

            list.Books.Remove(book);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<string>
            {
                IsSuccess = true,
                Result = "Book removed successfully"
            };
        }


        public async Task<ResponseModel<string>> RemoveListAsync(int listId)
        {
            var list = await _unitOfWork.UserLists.GetByIdAsync(listId);
            if (list == null)
            {
                return new ResponseModel<string>
                {
                    IsSuccess = false,
                    Message = "List not found"
                };
            }

            await _unitOfWork.UserLists.DeleteAsync(listId);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseModel<string>
            {
                IsSuccess = true,
                Result = "List deleted successfully"
            };
        }

    }
}
