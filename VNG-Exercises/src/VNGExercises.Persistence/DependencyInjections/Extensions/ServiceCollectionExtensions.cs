using AnyCodeBlog.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VNGExercises.Domain.Abstractions;
using VNGExercises.Domain.Abstractions.Repositories;
using VNGExercises.Persistence.DependencyInjections.Options;

namespace VNGExercises.Persistence.DependencyInjections.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddPostgreSql(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var options = provider.GetRequiredService<IOptionsMonitor<PostgreSqlRetryOptions>>();

            builder
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true)
                .UseLazyLoadingProxies(true)
                .UseNpgsql(
                    connectionString: configuration.GetConnectionString("VNGExercisesConnectionString"),
                    npgsqlOptionsAction: optionsBuilder
                                => optionsBuilder
                                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
        });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddRepositoryBaseConfigurations();
    }

    public static OptionsBuilder<PostgreSqlRetryOptions> ConfigurePostgreSqlRetryOptions(this IServiceCollection services, IConfigurationSection section)
        => services.AddOptions<PostgreSqlRetryOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart()
            ;

    public static void AddRepositoryBaseConfigurations(this IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
        services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
    }

}
