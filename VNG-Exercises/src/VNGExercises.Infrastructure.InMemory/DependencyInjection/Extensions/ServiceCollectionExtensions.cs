using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using VNGExercises.Infrastructure.InMemory.Abstractions;
using VNGExercises.Infrastructure.InMemory.Repositories;

namespace VNGExercises.Infrastructure.InMemory.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInMemoryRepositoryInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IInMemoryBookRepository, InMemoryBookRepository>();
            services.AddTransient<IInMemoryPostRepository, InMemoryPostRepository>();
            services.AddTransient<IInMemoryUserRepository, InMemoryUserRepository>();
        }
    }
}
