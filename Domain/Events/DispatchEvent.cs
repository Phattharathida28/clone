using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Domain.Events;
public static class DispatchEvent
{
    public static async Task Publish(DomainEvent domainEvent)
    {
        using (var serviceScope = ServiceActivator.GetScope())
        {
            domainEvent.IsPublished = true;
            IDomainEventService domainEventService = serviceScope.ServiceProvider.GetService<IDomainEventService>();
            await domainEventService.Publish(domainEvent);
        }
    }
}
