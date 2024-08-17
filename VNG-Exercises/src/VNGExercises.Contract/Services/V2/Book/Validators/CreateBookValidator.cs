using FluentValidation;

namespace VNGExercises.Contract.Services.V2.Book.Validators
{
    public class CreatePostValidator : AbstractValidator<Command.CreateBookCommand>
    {
        public CreatePostValidator()
        {
            RuleFor(t => t.Title).NotEmpty().NotNull();
            RuleFor(t => t.Author).NotEmpty().NotNull();
            RuleFor(t => t.PublishedAt).NotEmpty().NotNull().GreaterThan(DateTime.MinValue);
            RuleFor(t => t.CreatedBy).NotEmpty().NotNull();
        }
    }
}
