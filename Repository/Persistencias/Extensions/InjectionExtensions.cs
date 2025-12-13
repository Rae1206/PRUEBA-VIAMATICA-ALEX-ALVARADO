using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Context;
using Repository.Persistencias.Interfaces;
using Repository.Persistencias.Services;

namespace Repository.Persistencias.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(CineDbContext).Assembly.FullName;

            services.AddDbContext<CineDbContext>(
                option => option.UseSqlServer(
                    configuration.GetConnectionString("DBConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient
                );
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddTransient<IGenerateExcel, GenerateExcel>();
            //services.AddTransient<IAzureStorage, AzureStorage>();
            //services.AddTransient<IFileStorageLocal, FileStorageLocal>();

            return services;
        }
    }
}
