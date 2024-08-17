using VNGExercises.Domain.Entities;

namespace VNGExercises.Infrastructure.InMemory.Abstractions
{
    public interface IInMemoryBookRepository
    {
        public List<Book> FindAll(Func<Book, bool>? predicate = null);
        public Task<Book?> FindByIdAsync(Guid id);
        public void Add(Book entity);
        public Task RemoveAsync(Guid Id);
        public Task UpdateAsync(Book bookEntity);
    }
}
