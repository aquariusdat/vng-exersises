using MediatR;
using VNGExercises.Contract.Abstractions.Shared;

namespace VNGExercises.Contract.Abstractions.Message;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
