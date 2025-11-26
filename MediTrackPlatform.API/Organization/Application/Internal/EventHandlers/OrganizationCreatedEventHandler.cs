using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class OrganizationCreatedEventHandler : IEventHandler<OrganizationCreatedEvent>
{
    public Task Handle(OrganizationCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(OrganizationCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Organization: {0}", domainEvent.OrganizationId);
        return Task.CompletedTask;
    }
}