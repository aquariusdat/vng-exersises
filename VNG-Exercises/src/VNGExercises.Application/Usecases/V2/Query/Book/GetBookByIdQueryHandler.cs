using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V2.Book;
using VNGExercises.Domain.Exceptions;
using VNGExercises.Infrastructure.InMemory.Abstractions;

namespace VNGExercises.Application.Usecases.V2.Query.Book
{
    public class GetBookByIdQueryHandler : IQueryHandler<Contract.Services.V2.Book.Query.GetBookByIdQuery, Response.BookResponse>
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public GetBookByIdQueryHandler(IInMemoryBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<Result<Response.BookResponse>> Handle(Contract.Services.V2.Book.Query.GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var bookById = await _bookRepository.FindByIdAsync(request.Id) ?? throw new BookException.BookNotFoundException(request.Id);

            if (bookById.IsDeleted) throw new BookException.BookHasBeenDeletedException(request.Id);

            return _mapper.Map<Response.BookResponse>(bookById);
        }
    }
}
