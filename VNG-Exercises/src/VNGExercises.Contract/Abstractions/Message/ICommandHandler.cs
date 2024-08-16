using MediatR;
using VNGExercises.Contract.Abstractions.Shared;

namespace VNGExercises.Contract.Abstractions.Message;
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand, IDomainEvent
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
