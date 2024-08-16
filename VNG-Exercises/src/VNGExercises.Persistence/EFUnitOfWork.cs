using VNGExercises.Domain.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using VNGExercises.Domain.Abstractions;

namespace VNGExercises.Persistence;
public class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public EFUnitOfWork(ApplicationDbContext context)
        => _context = context;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync();
    async ValueTask IAsyncDisposable.DisposeAsync()
        => await _context.DisposeAsync();
}
