using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class InjectionExtensions
{
    public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Registrar servicios de aplicación
        services.AddScoped<IPeliculaApplication, PeliculaApplication>();
        services.AddScoped<ISalaCineApplication, SalaCineApplication>();
        services.AddScoped<IPeliculaSalaCineApplication, PeliculaSalaCineApplication>();
        services.AddScoped<IAuthApplication, AuthApplication>();

        return services;
    }
}
