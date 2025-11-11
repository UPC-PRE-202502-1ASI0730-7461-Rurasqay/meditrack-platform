using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

public record DeviceResource(int DeviceId, string Model, string Status, int HolderId, string HolderType);