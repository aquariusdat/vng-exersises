using VNGExercises.Domain.Abstractions.Entities;

namespace VNGExercises.Domain.Entities
{
    public class Post : DomainEntity<Guid>, IBaseAuditableEntity
    {
        public Post()
        {

        }

        private Post(Guid UserId, string Content, Guid CreatedBy)
        {
            Id = Guid.NewGuid();
            this.UserId = UserId;
            this.Content = Content;
            this.CreatedBy = CreatedBy;
            CreatedAt = DateTime.Now;
        }

        public static Post Create(Guid UserId, string Content, Guid CreatedBy) => new(UserId, Content, CreatedBy);

        public void Update(Guid UserId, string Content, Guid UpdatedBy)
        {
            this.UserId = UserId;
            this.Content = Content;
            this.UpdatedBy = UpdatedBy;
            UpdatedAt = DateTime.Now;
        }
        public void Delete(Guid DeletedBy)
        {
            this.DeletedBy = DeletedBy;
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }


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
