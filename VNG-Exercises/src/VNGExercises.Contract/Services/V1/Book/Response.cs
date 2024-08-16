namespace VNGExercises.Contract.Services.V1.Book
{
    public static class Response
    {
        public record BookResponse(Guid Id, string Title, string Author, DateTime PublishedAt);
    }
}
