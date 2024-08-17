using VNGExercises.Domain.Entities;

namespace VNGExercises.Infrastructure.InMemory
{
    public class InMemoryBookRepository
    {
        private List<Book> _books;

        public InMemoryBookRepository()
        {
            Random random = new Random();
            _books = new List<Book>();

            for (int i = 0; i <= 1000; i++)
            {
                _books.Add(new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = $"This is title {i}",
                    Author = $"Author{i}",
                    PublishedAt = new DateTime(random.Next(2000, 2024), random.Next(1, 12), random.Next(1, 25)),
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                });
            }
        }

        public List<Book> FindAll(Func<Book, bool>? predicate = null)
        {
            List<Book> items = _books; // Importance Always include AsNoTracking for Query Side
            if (predicate is not null)
            {
                items = items.Where(predicate).ToList();
            }
            return items;
        }

        public Task<Book?> FindByIdAsync(Guid id)
            => Task.FromResult(_books.FirstOrDefault(x => x.Id.Equals(id)));

        public void Add(Book entity)
            => _books.Add(entity);

        public Task RemoveAsync(Guid Id)
            => Task.FromResult(_books = _books.Where(t => t.Id != Id).ToList());

        public Task UpdateAsync(Book bookEntity)
        {
            var book = _books.Where(t => t.Id == bookEntity.Id).FirstOrDefault();

            if (book != null)
            {
                book.Title = bookEntity.Title;
                book.Author = bookEntity.Author;
            }

            return Task.CompletedTask;
        }
    }
}
