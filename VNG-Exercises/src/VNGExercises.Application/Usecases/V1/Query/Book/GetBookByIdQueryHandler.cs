using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V1.Book;
using VNGExercises.Domain.Abstractions.Repositories;
using VNGExercises.Domain.Exceptions;

namespace VNGExercises.Application.Usecases.V1.Query.Book
{
    public class GetBookByIdQueryHandler : IQueryHandler<Contract.Services.V1.Book.Query.GetBookByIdQuery, Response.BookResponse>
    {
        private readonly IRepositoryBase<Domain.Entities.Book, Guid> _bookRepository;
        private readonly IMapper _mapper;
        public GetBookByIdQueryHandler(IRepositoryBase<Domain.Entities.Book, Guid> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<Result<Response.BookResponse>> Handle(Contract.Services.V1.Book.Query.GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var bookById = await _bookRepository.FindByIdAsync(request.Id) ?? throw new BookException.BookNotFoundException(request.Id);

            if (bookById.IsDeleted) throw new BookException.BookHasBeenDeletedException(request.Id);

            return _mapper.Map<Response.BookResponse>(bookById);
        }
    }
}
