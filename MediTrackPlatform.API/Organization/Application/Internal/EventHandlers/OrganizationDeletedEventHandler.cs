using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class OrganizationDeletedEventHandler : IEventHandler<OrganizationDeletedEvent>
{
    public Task Handle(OrganizationDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(OrganizationDeletedEvent domainEvent)
    {
        Console.WriteLine("Deleted Organization: {0}", domainEvent.OrganizationId);
        return Task.CompletedTask;
    }
}