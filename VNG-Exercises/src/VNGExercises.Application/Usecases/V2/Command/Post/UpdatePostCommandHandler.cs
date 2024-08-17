using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V2.Post;
using VNGExercises.Domain.Abstractions.Repositories;
using VNGExercises.Domain.Exceptions;

namespace VNGExercises.Application.Usecases.V2.Command.Post
{
    public class UpdatePostCommandHandler : ICommandHandler<Contract.Services.V2.Post.Command.UpdatePostCommand, Contract.Services.V2.Post.Response.PostResponse>
    {
        private readonly IRepositoryBase<Domain.Entities.Post, Guid> _postRepository;
        private readonly IMapper _mapper;
        public UpdatePostCommandHandler(IRepositoryBase<Domain.Entities.Post, Guid> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<Result<Response.PostResponse>> Handle(Contract.Services.V2.Post.Command.UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.FindByIdAsync(request.Id) ?? throw new PostException.PostNotFoundException(request.Id);

            if (post.IsDeleted) throw new PostException.PostHasBeenDeletedException(post.Id);

            post.Update(request.UserId, request.Content, request.UpdatedBy);

            return _mapper.Map<Response.PostResponse>(post);
        }
    }
}
