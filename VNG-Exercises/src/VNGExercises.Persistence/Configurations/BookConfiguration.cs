using VNGExercises.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VNGExercises.Persistence.Constants;
using VNGExercises.Infrastructure.InMemory;

namespace VNGExercises.Persistence.Configurations;
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable(TableNames.Book);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Author).IsRequired();
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.PublishedAt).IsRequired();


        InMemoryBookRepository bookRepository = new InMemoryBookRepository();
        builder.HasData(bookRepository.FindAll());
    }
}
