using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class AdminUpdateEventHandler : IEventHandler<AdminUpdatedEvent>
{
    public Task Handle(AdminUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }
    
    private static Task On(AdminUpdatedEvent domainEvent)
    {
        Console.WriteLine("Updated Admin: {0}", domainEvent.AdminId);
        return Task.CompletedTask;
    }
}