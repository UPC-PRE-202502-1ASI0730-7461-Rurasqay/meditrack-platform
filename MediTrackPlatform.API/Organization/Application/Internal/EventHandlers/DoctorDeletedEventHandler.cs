using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class DoctorDeletedEventHandler : IEventHandler<DoctorDeletedEvent>
{
    public Task Handle(DoctorDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(DoctorDeletedEvent domainEvent)
    {
        Console.WriteLine("Deleted Doctor: {0}", domainEvent.DoctorId);
        return Task.CompletedTask;
    }
}