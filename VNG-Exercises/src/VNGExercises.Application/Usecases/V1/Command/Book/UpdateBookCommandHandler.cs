using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V1.Book;
using VNGExercises.Domain.Abstractions.Repositories;
using VNGExercises.Domain.Exceptions;

namespace VNGExercises.Application.Usecases.V1.Command.Book
{
    public class UpdateBookCommandHandler : ICommandHandler<Contract.Services.V1.Book.Command.UpdateBookCommand, Response.BookResponse>
    {
        private readonly IRepositoryBase<Domain.Entities.Book, Guid> _bookRepository;
        private readonly IMapper _mapper;
        public UpdateBookCommandHandler(IRepositoryBase<Domain.Entities.Book, Guid> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<Result<Response.BookResponse>> Handle(Contract.Services.V1.Book.Command.UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.FindByIdAsync(request.Id) ?? throw new BookException.BookNotFoundException(request.Id);

            if (book.IsDeleted) throw new BookException.BookHasBeenDeletedException(book.Id);

            book.Update(request.Title, request.Author, request.PublishedAt, request.UpdatedBy);

            return _mapper.Map<Response.BookResponse>(book);
        }
    }
}
