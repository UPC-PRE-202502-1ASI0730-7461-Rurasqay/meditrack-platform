using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class DoctorUpdatedEventHandler : IEventHandler<DoctorUpdatedEvent>
{
    public Task Handle(DoctorUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(DoctorUpdatedEvent domainEvent)
    {
        Console.WriteLine("Updated Doctor: {0}", domainEvent.DoctorId);
        return Task.CompletedTask;
    }
}