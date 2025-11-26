using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Domain.Model.Events;
using MediTrackPlatform.API.Devices.Domain.Services;
using MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

namespace MediTrackPlatform.API.Devices.Application.EventHandlers;

public class AlertCreatedEventHandler(IAlertCommandService alertCommandService): IEventHandler<AlertCreatedEvent>
{
    public Task Handle(AlertCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private async Task On(AlertCreatedEvent domainEvent)
    {
        var command = new CreateAlertCommand(
            domainEvent.DeviceId,
            domainEvent.DataRegistered,
            domainEvent.MeasurementType
        );
        
        await alertCommandService.Handle(command);
    }
}