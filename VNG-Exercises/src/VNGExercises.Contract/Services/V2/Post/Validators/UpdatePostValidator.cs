using FluentValidation;

namespace VNGExercises.Contract.Services.V2.Post.Validators
{
    public class UpdatePostValidator : AbstractValidator<Command.UpdatePostCommand>
    {
        public UpdatePostValidator()
        {
            RuleFor(t => t.UserId).NotEmpty().NotNull();
            RuleFor(t => t.Content).NotEmpty().NotNull();
            RuleFor(t => t.UpdatedBy).NotEmpty().NotNull();
        }
    }
}
