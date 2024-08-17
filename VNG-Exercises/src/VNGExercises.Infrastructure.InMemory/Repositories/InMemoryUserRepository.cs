using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VNGExercises.Domain.Entities;
using VNGExercises.Infrastructure.InMemory.Abstractions;

namespace VNGExercises.Infrastructure.InMemory.Repositories
{
    public class InMemoryUserRepository : IInMemoryUserRepository
    {
        private List<User> _users;

        public InMemoryUserRepository()
        {
            Random random = new Random();
            _users = new List<User>();

            for (int i = 0; i <= 1000; i++)
            {
                _users.Add(new User()
                {
                    Id = Guid.NewGuid(),
                    FullName = $"Hua Ton Dat {i}",
                    LastName = $"Hua Ton",
                    FirstName = $"Dat {i}",
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.NewGuid(),
                    Email = $"tondat.dev{i}@gmail.com",
                    Status = "NONE",
                    LastUpdatedPwd = new DateTime(2024, random.Next(1, 12), random.Next(1, 28))
                });
            }
        }

        public List<User> FindAll(Func<User, bool>? predicate = null)
        {
            List<User> items = _users; // Importance Always include AsNoTracking for Query Side
            if (predicate is not null)
            {
                items = items.Where(predicate).ToList();
            }
            return items;
        }

        public Task<User?> FindByIdAsync(Guid id)
            => Task.FromResult(_users.FirstOrDefault(x => x.Id.Equals(id)));

        public void Add(User entity)
            => _users.Add(entity);

        public Task RemoveAsync(Guid Id)
            => Task.FromResult(_users = _users.Where(t => t.Id != Id).ToList());

        public Task UpdateAsync(User userEntity)
        {
            var user = _users.Where(t => t.Id == userEntity.Id).FirstOrDefault();

            if (user != null)
            {
                user.FullName = userEntity.FullName;
                user.Email = userEntity.Email;
                user.FirstName = userEntity.FirstName;
                user.LastName = userEntity.LastName;
            }

            return Task.CompletedTask;
        }
    }
}
