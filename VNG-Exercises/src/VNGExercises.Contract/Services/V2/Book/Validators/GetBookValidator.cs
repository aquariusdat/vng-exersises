using FluentValidation;

namespace VNGExercises.Contract.Services.V2.Book.Validators
{
    public class GetPostValidator : AbstractValidator<Query.GetBookByIdQuery>
    {
        public GetPostValidator()
        {
            RuleFor(t => t.Id).NotNull().NotEmpty();
        }
    }
}
