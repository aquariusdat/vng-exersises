using MassTransit;
using MediatR;
using VNGExercises.Contract.Abstractions.Shared;

namespace VNGExercises.Contract.Abstractions.Message;
[ExcludeFromTopology]
public interface IDomainEvent : IRequest /*IMessage*/ /*INotification*/
{
    public Guid EventId { get; init; }
    public Guid Id { get; set; }
}
[ExcludeFromTopology]
public interface IDomainEvent<TResponse> : IRequest<Result<TResponse>>
{
    public Guid EventId { get; init; }
    public Guid Id { get; set; }
}

