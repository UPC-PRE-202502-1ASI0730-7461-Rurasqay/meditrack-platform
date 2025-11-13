using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class OrganizationUpdatedEventHandler : IEventHandler<OrganizationUpdatedEvent>
{
    public Task Handle(OrganizationUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(OrganizationUpdatedEvent domainEvent)
    {
        Console.WriteLine("Updated Organization: {0}", domainEvent.OrganizationId);
        return Task.CompletedTask;
    }
}