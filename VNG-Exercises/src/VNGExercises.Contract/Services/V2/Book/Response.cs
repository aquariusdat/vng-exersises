namespace VNGExercises.Contract.Services.V2.Book
{
    public static class Response
    {
        public record BookResponse(Guid Id, string Title, string Author, DateTime PublishedAt);
    }
}
