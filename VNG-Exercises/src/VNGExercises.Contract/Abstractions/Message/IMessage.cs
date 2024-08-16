using MediatR;

namespace VNGExercises.Contract.Abstractions.Message;

public interface IMessage : IRequest
{
    public Guid Id { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
}
