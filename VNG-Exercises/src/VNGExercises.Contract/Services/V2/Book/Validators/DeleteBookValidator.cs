using FluentValidation;

namespace VNGExercises.Contract.Services.V2.Book.Validators
{
    public class DeletePostValidator : AbstractValidator<Command.DeleteBookCommand>
    {
        public DeletePostValidator()
        {
            RuleFor(t => t.Id).NotNull().NotEmpty();
            RuleFor(t => t.DeletedBy).NotNull().NotEmpty();
        }
    }
}
