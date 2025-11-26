using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class CaregiverCreatedEventHandler : IEventHandler<CaregiverCreatedEvent>
{
    public Task Handle(CaregiverCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(CaregiverCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Caregiver: {0}", domainEvent.CaregiverId);
        return Task.CompletedTask;
    }
}