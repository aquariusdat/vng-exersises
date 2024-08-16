using VNGExercises.Domain.Abstractions.Entities;

namespace VNGExercises.Domain.Entities
{
    public class Book : DomainEntity<Guid>, IBaseAuditableEntity
    {
        public Book()
        {

        }
        private Book(string Title, string Author, DateTime PublishedAt, Guid CreatedBy)
        {
            Id = Guid.NewGuid();
            this.Title = Title;
            this.Author = Author;
            this.CreatedBy = CreatedBy;
            this.PublishedAt = PublishedAt;
            CreatedAt = DateTime.Now;
        }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedAt { get; set; }


        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Behaviors
        public static Book Create(string Title, string Author, DateTime PublishedAt, Guid CreatedBy) => new(Title, Author, PublishedAt, CreatedBy);
        public void Update(string Title, string Author, DateTime PublishedAt, Guid UpdatedBy)
        {
            this.Title = Title;
            this.Author = Author;
            this.PublishedAt = PublishedAt;
            this.UpdatedBy = UpdatedBy;
            UpdatedAt = DateTime.Now;
        }
        public void Delete(Guid DeletedBy)
        {
            this.DeletedBy = DeletedBy;
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }
    }
}
