using FluentValidation;

namespace VNGExercises.Contract.Services.V2.Book.Validators
{
    public class UpdatePostValidator : AbstractValidator<Command.UpdateBookCommand>
    {
        public UpdatePostValidator()
        {
            RuleFor(t => t.Id).NotEmpty().NotNull();
            RuleFor(t => t.Title).NotEmpty().NotNull();
            RuleFor(t => t.Author).NotEmpty().NotNull();
            RuleFor(t => t.PublishedAt).NotEmpty().NotNull();
            RuleFor(t => t.UpdatedBy).NotEmpty().NotNull();
        }
    }
}
