using VNGExercises.Contract.Abstractions.Message;

namespace VNGExercises.Contract.Services.V2.Post
{
    public static class Command
    {
        public record CreatePostCommand(Guid UserId, string Content, Guid CreatedBy) : ICommand<Contract.Services.V2.Post.Response.PostResponse>;
        public record UpdatePostCommand(Guid Id, Guid UserId, string Content, Guid UpdatedBy) : ICommand<Contract.Services.V2.Post.Response.PostResponse>;
        public record DeletePostCommand(Guid Id, Guid DeletedBy) : ICommand<bool>;
    }
}
