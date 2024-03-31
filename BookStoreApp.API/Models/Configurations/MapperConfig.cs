using AutoMapper;
using BookStoreApp.API.Models.Author;

namespace BookStoreApp.API.Models.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<AuthorCreateDto, Data.Author>().ReverseMap();
        CreateMap<AuthorReadOnlyDto, Data.Author>().ReverseMap();
        CreateMap<AuthorUpdateDto, Data.Author>().ReverseMap();
    }    
}