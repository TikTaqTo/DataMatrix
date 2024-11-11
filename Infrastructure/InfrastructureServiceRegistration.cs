using Application.Repository;
using Application.Repository.ContactRepository;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Repository.ContactRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(configuration["InMemoryDatabaseName"]));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}
