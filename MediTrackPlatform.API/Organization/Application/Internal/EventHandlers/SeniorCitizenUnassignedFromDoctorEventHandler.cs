using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class SeniorCitizenUnassignedFromDoctorEventHandler : IEventHandler<SeniorCitizenUnassignedFromDoctorEvent>
{
    public Task Handle(SeniorCitizenUnassignedFromDoctorEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(SeniorCitizenUnassignedFromDoctorEvent domainEvent)
    {
        Console.WriteLine("Unassigned Senior Citizen: {0} -/-> From Doctor: {1}", domainEvent.SeniorCitizenId, domainEvent.DoctorId);
        return Task.CompletedTask;
    }
}