using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V2.Book;
using VNGExercises.Domain.Exceptions;
using VNGExercises.Infrastructure.InMemory.Abstractions;

namespace VNGExercises.Application.Usecases.V2.Command.Book
{
    public class UpdateBookCommandHandler : ICommandHandler<Contract.Services.V2.Book.Command.UpdateBookCommand, Response.BookResponse>
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IInMemoryBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Result<Response.BookResponse>> Handle(Contract.Services.V2.Book.Command.UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.FindByIdAsync(request.Id) ?? throw new BookException.BookNotFoundException(request.Id);

            if (book.IsDeleted) throw new BookException.BookHasBeenDeletedException(book.Id);

            book.Update(request.Title, request.Author, request.PublishedAt, request.UpdatedBy);

            return _mapper.Map<Response.BookResponse>(book);
        }
    }
}
