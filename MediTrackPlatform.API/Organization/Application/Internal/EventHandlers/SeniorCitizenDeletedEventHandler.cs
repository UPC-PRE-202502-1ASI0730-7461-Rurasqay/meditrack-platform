using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class SeniorCitizenDeletedEventHandler : IEventHandler<SeniorCitizenDeletedEvent>
{
    public Task Handle(SeniorCitizenDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(SeniorCitizenDeletedEvent domainEvent)
    {
        Console.WriteLine("Deleted Senior Citizen: {0}", domainEvent.SeniorCitizenId);
        return Task.CompletedTask;
    }
}