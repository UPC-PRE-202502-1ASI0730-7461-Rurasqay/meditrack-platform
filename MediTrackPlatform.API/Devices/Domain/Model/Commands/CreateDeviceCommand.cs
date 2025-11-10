namespace MediTrackPlatform.API.Devices.Domain.Model.Commands;

public record CreateDeviceCommand(
    int DeviceId,
    string Model,
    int HolderId,
    string HolderType
    );