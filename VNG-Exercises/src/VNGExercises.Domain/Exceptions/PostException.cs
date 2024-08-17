namespace VNGExercises.Domain.Exceptions;

public static class PostException
{
    public class PostNotFoundException : NotFoundException
    {
        public PostNotFoundException(Guid postId)
            : base($"The post with the id {postId} was not found.") { }
    }

    public class PostHasBeenDeletedException : NotFoundException
    {
        public PostHasBeenDeletedException(Guid postId)
            : base($"The post with the id {postId} has been deleted.") { }
    }

    public class PostInsertFailedException : NotFoundException
    {
        public PostInsertFailedException(Guid postId)
            : base($"The post with the id {postId} insert failed.") { }
    }

    public class PostUpdateFailedException : NotFoundException
    {
        public PostUpdateFailedException(Guid postId)
            : base($"The post with the id {postId} update failed.") { }
    }
}
