using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V2.Post;
using VNGExercises.Domain.Abstractions.Repositories;

namespace VNGExercises.Application.Usecases.V2.Query.Post
{
    public class GetPostQueryHandler : IQueryHandler<Contract.Services.V2.Post.Query.GetPostQuery, PagedResult<Response.PostResponse>>
    {
        private readonly IRepositoryBase<Domain.Entities.Post, Guid> _postRepository;
        private readonly IMapper _mapper;

        public GetPostQueryHandler(IMapper mapper, IRepositoryBase<Domain.Entities.Post, Guid> postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<Result<PagedResult<Response.PostResponse>>> Handle(Contract.Services.V2.Post.Query.GetPostQuery request, CancellationToken cancellationToken)
        {
            var postsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                           ? _postRepository.FindAll(t => !t.IsDeleted)
                           : _postRepository.FindAll(x => x.Content.ToUpper().Contains(request.SearchTerm.ToUpper()) && !x.IsDeleted);

            var posts = await PagedResult<VNGExercises.Domain.Entities.Post>.CreateAsync(postsQuery, request.PageIndex, request.PageSize);
            var result = _mapper.Map<PagedResult<Response.PostResponse>>(posts);

            return Result.Success(result);
        }
    }
}
