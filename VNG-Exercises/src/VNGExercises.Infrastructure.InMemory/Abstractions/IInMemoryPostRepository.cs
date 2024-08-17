using VNGExercises.Domain.Entities;

namespace VNGExercises.Infrastructure.InMemory.Abstractions
{
    public interface IInMemoryPostRepository
    {
        public List<Post> FindAll(Func<Post, bool>? predicate = null);
        public Task<Post?> FindByIdAsync(Guid id);
        public void Add(Post entity);
        public Task RemoveAsync(Guid Id);
        public Task UpdateAsync(Post bookEntity);
    }
}
