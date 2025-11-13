using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class CaregiverDeletedEventHandler : IEventHandler<CaregiverDeletedEvent>
{
    public Task Handle(CaregiverDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(CaregiverDeletedEvent domainEvent)
    {
        Console.WriteLine("Deleted Caregiver: {0}", domainEvent.CaregiverId);
        return Task.CompletedTask;
    }
}