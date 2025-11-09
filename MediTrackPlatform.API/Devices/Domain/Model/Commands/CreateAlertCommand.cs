namespace MediTrackPlatform.API.Devices.Domain.Model.Commands;

using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;
public record CreateAlertCommand(
    string DeviceId,
    EAlertType Type,
    string Message,
    string DataRegistered,
    DateTime RegisteredAt
    );