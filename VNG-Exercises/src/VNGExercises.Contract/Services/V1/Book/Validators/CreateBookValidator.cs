using FluentValidation;

namespace VNGExercises.Contract.Services.V1.Book.Validators
{
    public class CreateBookValidator : AbstractValidator<Command.CreateBookCommand>
    {
        public CreateBookValidator()
        {
            RuleFor(t => t.Title).NotEmpty().NotNull();
            RuleFor(t => t.Author).NotEmpty().NotNull();
            RuleFor(t => t.PublishedAt).NotEmpty().NotNull().GreaterThan(DateTime.MinValue);
            RuleFor(t => t.CreatedBy).NotEmpty().NotNull();
        }
    }
}
