using VNGExercises.Contract.Abstractions.Message;

namespace VNGExercises.Contract.Services.V2.Book
{
    public static class Command
    {
        public record CreateBookCommand(string Title, string Author, DateTime PublishedAt, Guid CreatedBy) : ICommand<Contract.Services.V2.Book.Response.BookResponse>;
        public record UpdateBookCommand(Guid Id, string Title, string Author, DateTime PublishedAt, Guid UpdatedBy) : ICommand<Contract.Services.V2.Book.Response.BookResponse>;
        public record DeleteBookCommand(Guid Id, Guid DeletedBy) : ICommand<bool>;
    }
}
