using System.Threading.Tasks;

namespace Domain.Events;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
