using VNGExercises.Domain.Entities;
using VNGExercises.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace VNGExercises.Persistence;
public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder) =>
     builder.ApplyConfigurationsFromAssembly(VNGExercises.Persistence.AssemblyReference.Assembly);

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Follower> Followers { get; set; }
}
