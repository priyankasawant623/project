using DataWarehouse.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouse.API.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // EF Core DbContext - MySQL
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(
                    configuration.GetConnectionString("DefaultConnection")
                )
            )
        );

        // Repositories
        services.AddScoped<OrderRepository>();

        return services;
    }
}

