using JobApplication.Data;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace JobApplication.API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer<StoreContext>(configuration.GetConnectionString("DefaultConnection"));
        return services;
    }
    public static IServiceCollection AddJobApplicationServices(this IServiceCollection services)
    {
        var baseServiceType = typeof(IJobApplicationBaseService);

        // Get all types from all loaded assemblies that implement IJobApplicationBaseService
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsAbstract && baseServiceType.IsAssignableFrom(type));

        // Register each type as a scoped service
        foreach (var type in types)
        {
            services.AddScoped(type);
        }

        return services;
    }

    public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration conf)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["Token:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                });
        return services;
    }
}
