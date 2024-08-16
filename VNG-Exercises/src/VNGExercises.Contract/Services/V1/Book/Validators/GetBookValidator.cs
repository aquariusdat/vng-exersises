using FluentValidation;

namespace VNGExercises.Contract.Services.V1.Book.Validators
{
    public class GetBookValidator : AbstractValidator<Query.GetBookByIdQuery>
    {
        public GetBookValidator()
        {
            RuleFor(t => t.Id).NotNull().NotEmpty();
        }
    }
}
