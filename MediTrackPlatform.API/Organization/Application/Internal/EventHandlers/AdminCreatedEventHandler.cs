using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class AdminCreatedEventHandler : IEventHandler<AdminCreatedEvent>
{
    public Task Handle(AdminCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(AdminCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Admin: {0}", domainEvent.AdminId);
        return Task.CompletedTask;
    }
}