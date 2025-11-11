using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

public record CreateAlertResource(int DeviceId, EAlertType Type, string Message, string DataRegistered );