using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;

namespace VNGExercises.Contract.Services.V2.Post
{
    public static class Query
    {
        public record GetPostQuery(string? SearchTerm, int PageIndex, int PageSize) : IQuery<PagedResult<Contract.Services.V2.Post.Response.PostResponse>>;
        public record GetPostByIdQuery(Guid Id) : IQuery<Contract.Services.V2.Post.Response.PostResponse>;
    }
}
