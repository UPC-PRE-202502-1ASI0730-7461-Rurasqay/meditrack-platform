using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class CaregiverUpdatedEventHandler : IEventHandler<CaregiverUpdatedEvent>
{
    public Task Handle(CaregiverUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(CaregiverUpdatedEvent domainEvent)
    {
        Console.WriteLine("Updated Caregiver: {0}", domainEvent.CaregiverId);
        return Task.CompletedTask;
    }
}