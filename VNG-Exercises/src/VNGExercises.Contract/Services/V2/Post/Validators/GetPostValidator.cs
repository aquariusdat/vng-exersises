using FluentValidation;

namespace VNGExercises.Contract.Services.V2.Post.Validators
{
    public class GetPostValidator : AbstractValidator<Query.GetPostByIdQuery>
    {
        public GetPostValidator()
        {
            RuleFor(t => t.Id).NotNull().NotEmpty();
        }
    }
}
