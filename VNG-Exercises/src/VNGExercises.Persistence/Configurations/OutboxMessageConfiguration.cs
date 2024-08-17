using VNGExercises.Persistence.Constants;
using VNGExercises.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VNGExercises.Persistence.Configurations;
public class OutboxMessageConfiguration : IEntityTypeConfiguration<Outbox.OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TableNames.OutboxMessages);

        builder.HasKey(t => t.Id);
    }
}
