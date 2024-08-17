using AutoMapper;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Domain.Abstractions.Repositories;
using VNGExercises.Domain.Exceptions;

namespace VNGExercises.Application.Usecases.V1.Command.Book
{
    public class DeleteBookCommandHandler : ICommandHandler<Contract.Services.V1.Book.Command.DeleteBookCommand, bool>
    {
        private readonly IRepositoryBase<Domain.Entities.Book, Guid> _bookRepository;
        public DeleteBookCommandHandler(IRepositoryBase<Domain.Entities.Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<bool>> Handle(Contract.Services.V1.Book.Command.DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.FindByIdAsync(request.Id) ?? throw new BookException.BookNotFoundException(request.Id);

            if (book.IsDeleted) throw new BookException.BookHasBeenDeletedException(book.Id);

            book.Delete(request.Id);

            return Result.Success(true);
        }
    }
}
