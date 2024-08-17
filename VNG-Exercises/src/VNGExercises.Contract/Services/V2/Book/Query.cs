using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;

namespace VNGExercises.Contract.Services.V2.Book
{
    public static class Query
    {
        public record GetBookQuery(string? SearchTerm, int PageIndex, int PageSize) : IQuery<PagedResult<Contract.Services.V2.Book.Response.BookResponse>>;
        public record GetBookByIdQuery(Guid Id) : IQuery<Contract.Services.V2.Book.Response.BookResponse>;
    }
}
