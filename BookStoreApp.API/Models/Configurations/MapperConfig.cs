using AutoMapper;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;

namespace BookStoreApp.API.Models.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<AuthorCreateDto, Data.Author>().ReverseMap();
        CreateMap<AuthorReadOnlyDto, Data.Author>().ReverseMap();
        CreateMap<AuthorUpdateDto, Data.Author>().ReverseMap();

        CreateMap<BookCreateDto, Data.Book>().ReverseMap();
        CreateMap<BookUpdateDto, Data.Book>().ReverseMap();
        CreateMap<Data.Book, BookReadOnlyDto>()
            .ForMember(q => q.AuthorName,
                    d => d.MapFrom(m => $"{m.Author!.FirstName} {m.Author.LastName}"))
            .ReverseMap();
        CreateMap<Data.Book, BookDetailDto>()
            .ForMember(q => q.AuthorName,
                d => d.MapFrom(m => $"{m.Author!.FirstName} {m.Author.LastName}"))
            .ReverseMap();
    }      
}