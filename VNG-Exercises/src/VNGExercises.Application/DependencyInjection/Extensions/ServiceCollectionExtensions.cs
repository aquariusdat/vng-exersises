using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using VNGExercises.Application.Behaviors;
using VNGExercises.Application.Mapper;

namespace VNGExercises.Application.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMediatRApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPiplineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePiplineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));
            services.AddValidatorsFromAssembly(VNGExercises.Contract.AssemblyReference.Assembly, includeInternalTypes: true);
        }

        public static void AddAutoMapperApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceProfile));
        }
    }
}
