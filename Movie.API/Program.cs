
using Movie.API.Extensions;
using Movie.API.Infrastructure;
using Movie.API.Middlewares;
using Movie.Service.Utility;

namespace Movie.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration Configuration = builder.Configuration;
            IServiceCollection services = builder.Services;
            Settings setting = Configuration.Get<Settings>()!;
            if (setting == null)
            {
                throw new ArgumentNullException(nameof(setting));
            }

            OmdConfig? omdConfig = setting.OmdConfig;
            if (omdConfig == null)
            {
                throw new ArgumentNullException(nameof(omdConfig));
            }

            services.AddSingleton(setting);
            services.AddSingleton(omdConfig);
            // Add services to the container.
            string? connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.RegisterDbContext(connectionString);
            services.SetupAppServices();
            services.AddHttpClient();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(policyBuilder =>
                policyBuilder.AddDefaultPolicy(policy =>
                policy.WithOrigins("*")
                .AllowAnyHeader()
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader().SetIsOriginAllowed(origin => true)));
            var app = builder.Build();

            app.UseCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true)
           .AllowCredentials());
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
