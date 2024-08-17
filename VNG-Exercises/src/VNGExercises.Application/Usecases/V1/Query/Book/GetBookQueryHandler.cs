using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V1.Book;
using VNGExercises.Domain.Abstractions.Repositories;

namespace VNGExercises.Application.Usecases.V1.Query.Book
{
    public class GetBookQueryHandler : IQueryHandler<Contract.Services.V1.Book.Query.GetBookQuery, PagedResult<Response.BookResponse>>
    {
        private readonly IRepositoryBase<Domain.Entities.Book, Guid> _bookRepository;
        private readonly IMapper _mapper;

        public GetBookQueryHandler(IMapper mapper, IRepositoryBase<Domain.Entities.Book, Guid> bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<Result<PagedResult<Response.BookResponse>>> Handle(Contract.Services.V1.Book.Query.GetBookQuery request, CancellationToken cancellationToken)
        {
            var booksQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                           ? _bookRepository.FindAll(t => !t.IsDeleted)
                           : _bookRepository.FindAll(x => x.Title.ToUpper().Contains(request.SearchTerm.ToUpper()) && !x.IsDeleted);

            var books = await PagedResult<VNGExercises.Domain.Entities.Book>.CreateAsync(booksQuery, request.PageIndex, request.PageSize);
            var result = _mapper.Map<PagedResult<Response.BookResponse>>(books);

            return Result.Success(result);
        }
    }
}
