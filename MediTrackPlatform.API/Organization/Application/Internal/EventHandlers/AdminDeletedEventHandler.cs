using MediTrackPlatform.API.Organization.Domain.Model.Events;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Organization.Application.Internal.EventHandlers;

public class AdminDeletedEventHandler : IEventHandler<AdminDeletedEvent>
{
    public Task Handle(AdminDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(AdminDeletedEvent domainEvent)
    {
        Console.WriteLine("Deleted Admin: {0}", domainEvent.AdminId);
        return Task.CompletedTask;
    }
}