using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class SeniorCitizenAssignedToDoctorEventHandler : IEventHandler<SeniorCitizenAssignedToDoctorEvent>
{
    public Task Handle(SeniorCitizenAssignedToDoctorEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(SeniorCitizenAssignedToDoctorEvent domainEvent)
    {
        Console.WriteLine("Assigned Senior Citizen: {0} --> To Doctor: {1}", domainEvent.SeniorCitizenId, domainEvent.DoctorId);
        return Task.CompletedTask;
    }
}