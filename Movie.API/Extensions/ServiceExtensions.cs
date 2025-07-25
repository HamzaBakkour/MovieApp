using Microsoft.EntityFrameworkCore;
using Movie.Contracts;
using Movie.Core.DomainContracts;
using Movie.Data.DataConfigurations;
using Movie.Data.Repositories;
using Movie.Services;
namespace Movie.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });

            options.AddPolicy("AllowAll", p =>
               p.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
        });
    }


    public static void ConfigureSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));
    }



    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped(provider => new Lazy<IMovieRepository>(() => provider.GetRequiredService<IMovieRepository>()));

        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped(provider => new Lazy<IReviewRepository>(() => provider.GetRequiredService<IReviewRepository>()));
    }

    public static void AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();

        services.AddScoped<IMovieService, MovieService>(); 
        services.AddScoped(provider => new Lazy<IMovieService>(() => provider.GetRequiredService<IMovieService>()));

        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped(provider => new Lazy<IReviewService>(() => provider.GetRequiredService<IReviewService>()));
    }

}
