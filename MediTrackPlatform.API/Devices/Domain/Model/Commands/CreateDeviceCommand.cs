namespace MediTrackPlatform.API.Devices.Domain.Model.Commands;

public record CreateDeviceCommand(
    string Model,
    int HolderId
    );