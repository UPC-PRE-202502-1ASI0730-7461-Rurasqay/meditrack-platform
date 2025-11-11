namespace MediTrackPlatform.API.Devices.Domain.Model.Commands;

using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;
public record CreateAlertCommand(
    int DeviceId,
    double DataRegistered,
    string MeasurementType
    );