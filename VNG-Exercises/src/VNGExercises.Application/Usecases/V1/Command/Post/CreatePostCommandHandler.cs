using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V1.Post;
using VNGExercises.Domain.Abstractions.Repositories;

namespace VNGExercises.Application.Usecases.V1.Command.Post
{
    public class CreatePostCommandHandler : ICommandHandler<Contract.Services.V1.Post.Command.CreatePostCommand, Response.PostResponse>
    {
        private readonly IRepositoryBase<Domain.Entities.Post, Guid> _postRepository;
        private readonly IMapper _mapper;
        public CreatePostCommandHandler(IRepositoryBase<Domain.Entities.Post, Guid> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<Result<Response.PostResponse>> Handle(Contract.Services.V1.Post.Command.CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = Domain.Entities.Post.Create(request.UserId, request.Content, request.CreatedBy);
            _postRepository.Add(post);

            var response = _mapper.Map<Response.PostResponse>(post);

            return response;
        }
    }
}
