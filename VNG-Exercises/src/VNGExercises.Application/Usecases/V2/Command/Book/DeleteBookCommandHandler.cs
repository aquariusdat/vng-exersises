using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Domain.Exceptions;
using VNGExercises.Infrastructure.InMemory.Abstractions;

namespace VNGExercises.Application.Usecases.V2.Command.Book
{
    public class DeleteBookCommandHandler : ICommandHandler<Contract.Services.V2.Book.Command.DeleteBookCommand, bool>
    {
        private readonly IInMemoryBookRepository _bookRepository;
        public DeleteBookCommandHandler(IInMemoryBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<bool>> Handle(Contract.Services.V2.Book.Command.DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.FindByIdAsync(request.Id) ?? throw new BookException.BookNotFoundException(request.Id);

            if (book.IsDeleted) throw new BookException.BookHasBeenDeletedException(book.Id);

            book.Delete(request.Id);

            return Result.Success(true);
        }
    }
}
