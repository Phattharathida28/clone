using Application.Interfaces;
using Domain.Entities.SU;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistense.Identity;

namespace Persistense;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CleanDbContext>((provider, opt) => opt.AddInterceptors(new NoLockInterceptor()).UseNpgsql(configuration.GetConnectionString("SQL")), ServiceLifetime.Scoped);
        services.AddScoped<ICleanDbContext>(provider => provider.GetService<CleanDbContext>());
        services.AddTransient<IJwtHandler, JwtHandler>();
        services.AddScoped<DataProtectorTokenProvider<User>>();
        services.AddIdentityCore<User>().AddEntityFrameworkStores<CleanDbContext>().AddDefaultTokenProviders();
        services.AddScoped<IIdentityService, IdentityService>();
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
        });

        services.AddAuthentication();

        return services;
    }
}
