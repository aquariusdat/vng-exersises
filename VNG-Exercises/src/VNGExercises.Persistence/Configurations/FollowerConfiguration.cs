using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VNGExercises.Domain.Entities;
using VNGExercises.Persistence.Constants;

namespace VNGExercises.Persistence.Configurations
{
    public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.ToTable(TableNames.Follower);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            //builder.Property(x => x.).IsRequired();
            //builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            //builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            //builder.Property(x => x.Content).HasMaxLength(100).IsRequired();
        }
    }
}
