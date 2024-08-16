using VNGExercises.Domain.Abstractions.Entities;

namespace VNGExercises.Domain.Entities
{
    public class Book : DomainEntity<Guid>, IBaseAuditableEntity
    {
        public string Title { get; set; }
        public Guid Author { get; set; }
        public int PublishedYear { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime CreateddAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
