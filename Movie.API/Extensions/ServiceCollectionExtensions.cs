using Microsoft.EntityFrameworkCore;
using Movie.Service.Implementation;
using Movie.Service.Interface;
using Movie.Core.AppDbContext;

namespace Movie.API.Extensions;

public static class ServiceCollectionExtensions
{

    public static void SetupAppServices(this IServiceCollection services)
    {

        services.AddTransient<IMovieService, MovieService>();

    }

    public static void RegisterDbContext(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, s =>
            {
                s.MigrationsAssembly("Movie.Core");
                s.EnableRetryOnFailure(3);
            });
        });
    }






}
