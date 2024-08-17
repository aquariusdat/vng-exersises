using AutoMapper;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V1.Book;
using VNGExercises.Domain.Abstractions.Repositories;

namespace VNGExercises.Application.Usecases.V1.Command.Book
{
    public class CreateBookCommandHandler : Contract.Abstractions.Message.ICommandHandler<Contract.Services.V1.Book.Command.CreateBookCommand, Response.BookResponse>
    {
        private readonly IRepositoryBase<Domain.Entities.Book, Guid> _bookRepository;
        private readonly IMapper _mapper;
        public CreateBookCommandHandler(IRepositoryBase<Domain.Entities.Book, Guid> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<Result<Response.BookResponse>> Handle(Contract.Services.V1.Book.Command.CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = Domain.Entities.Book.Create(request.Title, request.Author, request.PublishedAt, request.CreatedBy);
            _bookRepository.Add(book);

            var response = _mapper.Map<Response.BookResponse>(book);

            return response;
        }
    }
}
