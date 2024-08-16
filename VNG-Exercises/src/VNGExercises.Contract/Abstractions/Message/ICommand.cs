using MediatR;
using VNGExercises.Contract.Abstractions.Shared;

namespace VNGExercises.Contract.Abstractions.Message;
public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
