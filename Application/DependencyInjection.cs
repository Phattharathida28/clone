using Application.Behaviors;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        services.Scan(scan => scan.FromAssemblyOf<IService>().AddClasses(classes => classes.AssignableTo<IService>()).AsSelfWithInterfaces().WithScopedLifetime());

        return services;
    }
}
