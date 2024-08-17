namespace VNGExercises.Contract.Abstractions.Message
{
    public interface IDomainEvent
    {
        public Guid EventId { get; set; }
        public Guid Id { get; set; } // Id of entity
        public DateTime TimeStamp { get; set; }
    }
}
