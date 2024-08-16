namespace VNGExercises.Domain.Abstractions;
public interface IUnitOfWork : IAsyncDisposable
{
    /// <summary>
    /// Call save change from dbcontext
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
