using FluentValidation;

namespace VNGExercises.Contract.Services.V1.Book.Validators
{
    public class UpdateBookValidator : AbstractValidator<Command.UpdateBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(t => t.Id).NotEmpty().NotNull();
            RuleFor(t => t.Title).NotEmpty().NotNull();
            RuleFor(t => t.Author).NotEmpty().NotNull();
            RuleFor(t => t.PublishedAt).NotEmpty().NotNull();
            RuleFor(t => t.UpdatedBy).NotEmpty().NotNull();
        }
    }
}
