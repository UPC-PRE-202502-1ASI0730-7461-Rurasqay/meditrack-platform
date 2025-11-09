namespace MediTrackPlatform.API.Devices.Domain.Model.Commands;

public record CreateDeviceCommand(
    string DeviceId,
    string Model,
    string HolderId,
    string HolderType
    );