using MediTrackPlatform.API.Shared.Domain.Model.Events;

namespace MediTrackPlatform.API.Devices.Domain.Model.Events;

public class AlertCreatedEvent(int deviceId, double dataRegistered, string measurementType): IEvent
{
    public int DeviceId { get; } = deviceId;
    public double DataRegistered { get; } = dataRegistered;
    public string MeasurementType { get; } = measurementType;
}