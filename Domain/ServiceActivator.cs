using Microsoft.Extensions.DependencyInjection;
using System;


namespace Domain
{
    public class ServiceActivator
    {
        internal static IServiceProvider _serviceProvider = null;
        public static void Configure(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
        public static IServiceScope GetScope(IServiceProvider serviceProvider = null) => (serviceProvider ?? _serviceProvider)?.GetRequiredService<IServiceScopeFactory>().CreateScope();
    }
}
