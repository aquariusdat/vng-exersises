using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V2.Book;
using VNGExercises.Infrastructure.InMemory.Abstractions;

namespace VNGExercises.Application.Usecases.V2.Query.Book
{
    public class GetBookQueryHandler : IQueryHandler<Contract.Services.V2.Book.Query.GetBookQuery, PagedResult<Response.BookResponse>>
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookQueryHandler(IMapper mapper, IInMemoryBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<Result<PagedResult<Response.BookResponse>>> Handle(Contract.Services.V2.Book.Query.GetBookQuery request, CancellationToken cancellationToken)
        {
            var booksQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                           ? _bookRepository.FindAll(t => !t.IsDeleted)
                           : _bookRepository.FindAll(x => x.Title.ToUpper().Contains(request.SearchTerm.ToUpper()) && !x.IsDeleted);

            booksQuery = booksQuery.Skip(((request.PageIndex <= 0 ? 1 : request.PageIndex) - 1) * request.PageSize).Take(request.PageSize).ToList();

            var books = PagedResult<VNGExercises.Domain.Entities.Book>.Create(booksQuery, request.PageIndex, request.PageSize, booksQuery.Count);
            var result = _mapper.Map<PagedResult<Response.BookResponse>>(books);
            return Result.Success(result);
        }
    }
}
