using Microsoft.Extensions.DependencyInjection;
using GbLib.BuildingBlock.Domain.Interfaces;
using GbLib.BuildingBlock.Infrastructure.Interceptors;
using OrderManagement.Application.Services;

namespace OrderManagement.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentTenantProvider, CurrentTenantProvider>();
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        services.AddScoped<AuditInterceptor>();
        services.AddScoped<TenantInterceptor>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly));

        return services;
    }
}
