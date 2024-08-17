using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNGExercises.Contract.Abstractions.Shared;
using VNGExercises.Contract.Services.V2.Book;
using VNGExercises.Presentation.Abstractions;

namespace VNGExercises.Presentation.Controllers.V2
{
    [ApiVersion(2)]
    public class BookController : ApiController
    {
        public BookController(ISender sender) : base(sender)
        {
        }


        [HttpGet(Name = "GetBooks")]
        [ProducesResponseType(typeof(Result<IEnumerable<Response.BookResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Books(string? searchTerm = null, int pageIndex = 1, int pageSize = 10)
        {
            var result = await Sender.Send(new Query.GetBookQuery(searchTerm, pageIndex, pageSize));

            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType(typeof(Result<Response.BookResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Books(Guid bookId)
        {
            var result = await Sender.Send(new Query.GetBookByIdQuery(bookId));

            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpPost(Name = "CreateBook")]
        [ProducesResponseType(typeof(Result<Response.BookResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Books([FromBody] Command.CreateBookCommand createBook)
        {
            var result = await Sender.Send(createBook);

            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpPut(Name = "UpdateBook")]
        [ProducesResponseType(typeof(Result<Response.BookResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Books(Guid bookId, [FromBody] Command.UpdateBookCommand updateBook)
        {
            var result = await Sender.Send(new Command.UpdateBookCommand(bookId, updateBook.Title, updateBook.Author, updateBook.PublishedAt, updateBook.UpdatedBy));

            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }

        [HttpDelete(Name = "DeleteBook")]
        [ProducesResponseType(typeof(Result<Response.BookResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Books(Guid bookId, [FromBody] Command.DeleteBookCommand deleteBook)
        {
            var result = await Sender.Send(new Command.DeleteBookCommand(bookId, deleteBook.DeletedBy));

            if (result.IsFailure)
                return HandlerFailure(result);

            return Ok(result);
        }
    }
}
