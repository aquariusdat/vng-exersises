using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;

namespace VNGExercises.Contract.Services.V1.Book
{
    public static class Query
    {
        public record GetBookQuery(string? SearchTerm, int PageIndex, int PageSize) : IQuery<PagedResult<Contract.Services.V1.Book.Response.BookResponse>>;
        public record GetBookByIdQuery(Guid Id) : IQuery<Contract.Services.V1.Book.Response.BookResponse>;
    }
}
