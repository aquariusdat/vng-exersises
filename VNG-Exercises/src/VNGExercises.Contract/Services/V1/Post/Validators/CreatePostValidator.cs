using FluentValidation;

namespace VNGExercises.Contract.Services.V1.Post.Validators
{
    public class CreatePostValidator : AbstractValidator<Command.CreatePostCommand>
    {
        public CreatePostValidator()
        {
            RuleFor(t => t.UserId).NotEmpty().NotNull();
            RuleFor(t => t.Content).NotEmpty().NotNull();
            RuleFor(t => t.CreatedBy).NotEmpty().NotNull();
        }
    }
}
