using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V2.Post;
using VNGExercises.Domain.Abstractions.Repositories;
using VNGExercises.Domain.Exceptions;

namespace VNGExercises.Application.Usecases.V2.Query.Post
{
    public class GetPostByIdQueryHandler : IQueryHandler<Contract.Services.V2.Post.Query.GetPostByIdQuery, Response.PostResponse>
    {
        private readonly IRepositoryBase<Domain.Entities.Post, Guid> _postRepository;
        private readonly IMapper _mapper;
        public GetPostByIdQueryHandler(IRepositoryBase<Domain.Entities.Post, Guid> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<Result<Response.PostResponse>> Handle(Contract.Services.V2.Post.Query.GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var postById = await _postRepository.FindByIdAsync(request.Id) ?? throw new PostException.PostNotFoundException(request.Id);

            if (postById.IsDeleted) throw new PostException.PostHasBeenDeletedException(request.Id);

            return _mapper.Map<Response.PostResponse>(postById);
        }
    }
}
