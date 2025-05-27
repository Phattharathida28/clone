using Application.Interfaces;
using Domain.Events;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services.BackgroundTask;
using Microsoft.AspNetCore.SignalR;
using Infrastructure.Services.Hubs;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddHttpContextAccessor();
        services.AddHttpClient<IHttpService, HttpService>();
        services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
        services.AddEasyCaching(options =>
        {
            options.UseInMemory("default");
        });
        services.AddSingleton<ICacheService, CacheService>();

        services.AddHostedService<QueuedHostedService>();
        services.AddSingleton<IBackgroundTaskQueue>(ctx =>
        {
            if (!int.TryParse(configuration["QueueCapacity"], out var queueCapacity))
                queueCapacity = 100;
            return new BackgroundTaskQueue(queueCapacity,ctx.GetRequiredService<IHubContext<JobHub>>());
        });
        services.AddScoped<IActivityLogService, ActivityLogService>();

        return services;
    }
}
