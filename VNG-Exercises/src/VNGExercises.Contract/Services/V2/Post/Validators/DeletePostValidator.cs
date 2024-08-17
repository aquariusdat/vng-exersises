using FluentValidation;

namespace VNGExercises.Contract.Services.V2.Post.Validators
{
    public class DeletePostValidator : AbstractValidator<Command.DeletePostCommand>
    {
        public DeletePostValidator()
        {
            RuleFor(t => t.Id).NotNull().NotEmpty();
            RuleFor(t => t.DeletedBy).NotNull().NotEmpty();
        }
    }
}
