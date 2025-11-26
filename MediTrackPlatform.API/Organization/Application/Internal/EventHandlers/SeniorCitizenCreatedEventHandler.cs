using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class SeniorCitizenCreatedEventHandler : IEventHandler<SeniorCitizenCreatedEvent>
{
    public Task Handle(SeniorCitizenCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(SeniorCitizenCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Senior Citizen: {0}", domainEvent.SeniorCitizenId);
        return Task.CompletedTask;
    }
}