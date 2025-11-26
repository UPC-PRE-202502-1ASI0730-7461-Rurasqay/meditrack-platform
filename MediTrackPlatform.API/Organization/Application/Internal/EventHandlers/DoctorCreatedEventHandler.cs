using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class DoctorCreatedEventHandler : IEventHandler<DoctorCreatedEvent>
{
    public Task Handle(DoctorCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(DoctorCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Doctor: {0}", domainEvent.DoctorId);
        return Task.CompletedTask;
    }
}