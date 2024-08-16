using VNGExercises.Domain.Abstractions.Entities;

namespace VNGExercises.Domain.Entities
{
    public class Post : DomainEntity<Guid>, IBaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public string Content { get; set; }

        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
