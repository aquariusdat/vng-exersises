using FluentValidation;

namespace VNGExercises.Contract.Services.V1.Book.Validators
{
    public class DeleteBookValidator : AbstractValidator<Command.DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            RuleFor(t => t.Id).NotNull().NotEmpty();
            RuleFor(t => t.DeletedBy).NotNull().NotEmpty();
        }
    }
}
