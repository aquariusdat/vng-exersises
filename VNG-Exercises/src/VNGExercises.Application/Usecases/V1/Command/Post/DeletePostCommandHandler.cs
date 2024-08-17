using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Domain.Abstractions.Repositories;
using VNGExercises.Domain.Exceptions;

namespace VNGExercises.Application.Usecases.V1.Command.Post
{
    public class DeletePostCommandHandler : ICommandHandler<Contract.Services.V1.Post.Command.DeletePostCommand, bool>
    {
        private readonly IRepositoryBase<Domain.Entities.Post, Guid> _postRepository;
        public DeletePostCommandHandler(IRepositoryBase<Domain.Entities.Post, Guid> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Result<bool>> Handle(Contract.Services.V1.Post.Command.DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.FindByIdAsync(request.Id) ?? throw new PostException.PostNotFoundException(request.Id);

            if (post.IsDeleted) throw new PostException.PostHasBeenDeletedException(post.Id);

            post.Delete(request.Id);

            return Result.Success(true);
        }
    }
}
