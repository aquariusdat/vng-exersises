using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;
using VNG_Exercises.DependencyInjection.Extensions;
using VNGExercises.Persistence.DependencyInjections.Options;
using VNGExercises.Persistence.DependencyInjections.Extensions;
using VNGExercises.Application.DependencyInjection.Extensions;
using VNGExercises.Middlewares;
using VNGExercises.Infrastructure.DependencyInjection.Extensions;
using VNGExercises.Infrastructure.InMemory.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// SeriLog configurations
Log.Logger = new LoggerConfiguration().ReadFrom
                .Configuration(builder.Configuration)
                .CreateLogger();
builder.Logging.ClearProviders().AddSerilog();
builder.Host.UseSerilog();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Add services to the container.
builder.Services.AddControllers().AddApplicationPart(VNGExercises.Presentation.AssemblyReference.Assembly);
builder.Services.AddAutoMapperApplication();
builder.Services.AddMediatRApplication();

#region Add Swagger configrations
builder.Services
        .AddSwaggerGenNewtonsoftSupport()
        .AddFluentValidationRulesToSwagger()
        .AddEndpointsApiExplorer()
        .AddSwagger();
builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
#endregion

#region Infrastructure 
builder.Services.AddPostgreSql();
builder.Services.AddInMemoryRepositoryInfrastructure();

builder.Services.ConfigurePostgreSqlRetryOptions(builder.Configuration.GetSection(nameof(PostgreSqlRetryOptions)));

builder.Services.AddBackgroundJobInfrastructure(builder.Configuration);
builder.Services.AddQuartzInfrastructure();
#endregion

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    });
    //app.ConfigureSwagger();
}

app.UseAuthorization();

app.MapControllers();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly.");
}
catch (Exception ex)
{
    Log.Fatal("An unhandled exception occured during bootstrapping.");
    await app.StopAsync();
}
finally
{
    await Log.CloseAndFlushAsync();
    await app.DisposeAsync();
}

//app.Run();
