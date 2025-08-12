using Domain.Abstractions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddDbContext<HealthGoalDbContext>(opt =>
                opt.UseNpgsql(cfg.GetConnectionString("HealthGoalDb"))
                   .UseSnakeCaseNamingConvention()   // <<< สำคัญ
            );

            services.AddScoped<IGoalRepository, GoalRepository>();
            return services;
        }
    }
}
