using VNGExercises.Domain.Entities;

namespace VNGExercises.Infrastructure.InMemory.Abstractions
{
    public interface IInMemoryUserRepository
    {
        public List<User> FindAll(Func<User, bool>? predicate = null);
        public Task<User?> FindByIdAsync(Guid id);
        public void Add(User entity);
        public Task RemoveAsync(Guid Id);
        public Task UpdateAsync(User bookEntity);
    }
}
