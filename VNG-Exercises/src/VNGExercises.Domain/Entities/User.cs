using VNGExercises.Domain.Abstractions.Entities;

namespace VNGExercises.Domain.Entities
{
    public class User : DomainEntity<Guid>, IBaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime? LastUpdatedPwd { get; set; }
        public bool IsRequiredUpdatedPwd { get; set; }

        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Follower> Followers { get; set; }
    }
}

