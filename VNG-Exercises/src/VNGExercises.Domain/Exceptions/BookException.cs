namespace VNGExercises.Domain.Exceptions;

public static class BookException
{
    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(Guid bookId)
            : base($"The book with the id {bookId} was not found.") { }
    }

    public class BookHasBeenDeletedException : NotFoundException
    {
        public BookHasBeenDeletedException(Guid bookId)
            : base($"The book with the id {bookId} has been deleted.") { }
    }

    public class BookInsertFailedException : NotFoundException
    {
        public BookInsertFailedException(Guid bookId)
            : base($"The book with the id {bookId} insert failed.") { }
    }

    public class BookUpdateFailedException : NotFoundException
    {
        public BookUpdateFailedException(Guid bookId)
            : base($"The book with the id {bookId} update failed.") { }
    }
}
