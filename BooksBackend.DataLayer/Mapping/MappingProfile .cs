using AutoMapper;
using BooksBackend.DataLayer.Entities;
using BooksBackend.DataLayer.Dto.Users;
using BooksBackend.DataLayer.Dto.Books;
using BooksBackend.DataLayer.Dto.BookGenre;
using BooksBackend.DataLayer.Dto.Lists;
using BooksBackend.DataLayer.Dto.Reviews;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, GetUserDto>().ReverseMap();
        CreateMap<User, CreateOrUpdateBookDto>().ReverseMap(); 


        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<BookGenre, GetBookGenreDto>().ReverseMap();
        CreateMap<Book, GetBookDtoByID>().ReverseMap();
        CreateMap<Book, GetBookDto>().ReverseMap();
        CreateMap<Book, CreateOrUpdateBookDto>().ReverseMap();

        CreateMap<UserList, GetListDto>().ReverseMap();
        CreateMap<UserList, GetListsWithBooksDto>().ReverseMap();

        CreateMap<CreateReviewDto, Review>().ReverseMap();
        CreateMap<UpdateReviewDto, Review>().ReverseMap();
        CreateMap<GetReviewDto, Review>().ReverseMap();

    }
}
