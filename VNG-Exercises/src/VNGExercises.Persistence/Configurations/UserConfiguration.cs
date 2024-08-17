using VNGExercises.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VNGExercises.Persistence.Constants;
using VNGExercises.Infrastructure.InMemory;

namespace VNGExercises.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(x => x.Id);
        builder.HasIndex(t => t.Email).IsUnique();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        builder.Property(x => x.Email).IsRequired(true);


        InMemoryUserRepository inMemoryUserRepository = new InMemoryUserRepository();
        builder.HasData(inMemoryUserRepository.FindAll());

        // Each User can have many Followers
        builder.HasMany(e => e.Followers)
            .WithOne()
            .HasForeignKey(uc => uc.Id)
            .IsRequired();
    }
}
