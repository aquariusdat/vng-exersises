using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VNGExercises.Domain.Entities;
using VNGExercises.Infrastructure.InMemory.Repositories;
using VNGExercises.Persistence.Constants;

namespace VNGExercises.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable(TableNames.Post);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.Content).HasMaxLength(100).IsRequired();

            InMemoryPostRepository inMemoryPostRepository = new InMemoryPostRepository();
            builder.HasData(inMemoryPostRepository.FindAll());
        }
    }
}
