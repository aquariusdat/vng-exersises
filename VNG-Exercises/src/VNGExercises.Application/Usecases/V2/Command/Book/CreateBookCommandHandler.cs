using AutoMapper;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V2.Book;
using VNGExercises.Infrastructure.InMemory.Abstractions;

namespace VNGExercises.Application.Usecases.V2.Command.Book
{
    public class CreateBookCommandHandler : Contract.Abstractions.Message.ICommandHandler<Contract.Services.V2.Book.Command.CreateBookCommand, Contract.Services.V2.Book.Response.BookResponse>
    {
        private readonly IInMemoryBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public CreateBookCommandHandler(IInMemoryBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<Result<Response.BookResponse>> Handle(Contract.Services.V2.Book.Command.CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = Domain.Entities.Book.Create(request.Title, request.Author, request.PublishedAt, request.CreatedBy);
            _bookRepository.Add(book);

            var response = _mapper.Map<Contract.Services.V2.Book.Response.BookResponse>(book);
            return response;
        }
    }
}
