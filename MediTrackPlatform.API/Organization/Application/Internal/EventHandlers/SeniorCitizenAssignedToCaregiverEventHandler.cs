using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class SeniorCitizenAssignedToCaregiverEventHandler : IEventHandler<SeniorCitizenAssignedToCaregiverEvent>
{
    public Task Handle(SeniorCitizenAssignedToCaregiverEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(SeniorCitizenAssignedToCaregiverEvent domainEvent)
    {
        Console.WriteLine("Assigned Senior Citizen: {0} --> To Caregiver: {1}", domainEvent.SeniorCitizenId, domainEvent.CaregiverId);
        return Task.CompletedTask;
    }
}