using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Infrastructure.Persistence;
using TaskManagementApp.ServiceBus;

namespace TaskManagementApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceBusSender, ServiceBusSender>();

            return services;
        }
    }
}
