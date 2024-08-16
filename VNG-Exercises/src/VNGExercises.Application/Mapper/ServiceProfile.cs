using AutoMapper;
using VNGExercises.Domain.Entities;
using VNGExercises.Contract.Abstractions.Shared;

namespace VNGExercises.Application.Mapper;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        // V1
        CreateMap<Book, Contract.Services.V1.Book.Response.BookResponse>().ReverseMap();
        CreateMap<PagedResult<Book>, PagedResult<Contract.Services.V1.Book.Response.BookResponse>>().ReverseMap();

        //CreateMap<Post, Contract.Services.V1.Post.Response.PostResponse>().ReverseMap();
        //CreateMap<Result<List<Post>>, Result<List<Contract.Services.V1.Post.Response.PostResponse>>>().ReverseMap();
    }
}
