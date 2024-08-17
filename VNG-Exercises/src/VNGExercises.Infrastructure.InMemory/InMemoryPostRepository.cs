using VNGExercises.Domain.Entities;

namespace VNGExercises.Infrastructure.InMemory
{
    public class InMemoryPostRepository
    {
        private List<Post> _posts;

        public InMemoryPostRepository()
        {
            _posts = new List<Post>();

            for (int i = 0; i <= 1000; i++)
            {
                _posts.Add(new Post()
                {
                    Id = Guid.NewGuid(),
                    Content = $"This is content {i}",
                    UserId = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                });
            }
        }

        public List<Post> FindAll(Func<Post, bool>? predicate = null)
        {
            List<Post> items = _posts; // Importance Always include AsNoTracking for Query Side
            if (predicate is not null)
            {
                items = items.Where(predicate).ToList();
            }
            return items;
        }

        public Task<Post?> FindByIdAsync(Guid id)
            => Task.FromResult(_posts.FirstOrDefault(x => x.Id.Equals(id)));

        public void Add(Post entity)
            => _posts.Add(entity);

        public Task RemoveAsync(Guid Id)
            => Task.FromResult(_posts = _posts.Where(t => t.Id != Id).ToList());

        public Task UpdateAsync(Post postEntity)
        {
            var post = _posts.Where(t => t.Id == postEntity.Id).FirstOrDefault();

            if (post != null)
            {
                post.Content = postEntity.Content;
                post.UserId = postEntity.UserId;
            }

            return Task.CompletedTask;
        }
    }
}
