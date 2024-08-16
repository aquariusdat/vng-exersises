using MediatR;
using VNGExercises.Contract.Abstractions.Shared;

namespace VNGExercises.Contract.Abstractions.Message;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}

