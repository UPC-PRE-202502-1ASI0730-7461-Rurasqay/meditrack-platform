using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class SeniorCitizenUpdatedEventHandler : IEventHandler<SeniorCitizenUpdatedEvent>
{
    public Task Handle(SeniorCitizenUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(SeniorCitizenUpdatedEvent domainEvent)
    {
        Console.WriteLine("Updated Senior Citizen: {0}", domainEvent.SeniorCitizenId);
        return Task.CompletedTask;
    }
}