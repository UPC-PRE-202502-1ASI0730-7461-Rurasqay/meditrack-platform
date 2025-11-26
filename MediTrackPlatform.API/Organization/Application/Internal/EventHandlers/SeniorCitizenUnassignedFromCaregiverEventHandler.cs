using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class SeniorCitizenUnassignedFromCaregiverEventHandler : IEventHandler<SeniorCitizenUnassignedFromCaregiverEvent>
{
    public Task Handle(SeniorCitizenUnassignedFromCaregiverEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(SeniorCitizenUnassignedFromCaregiverEvent domainEvent)
    {
        Console.WriteLine("Unassigned Senior Citizen: {0} -/-> From Caregiver: {1}", domainEvent.SeniorCitizenId, domainEvent.CaregiverId);
        return Task.CompletedTask;
    }
}