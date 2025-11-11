using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Resources;
public record AlertResource(string AlertId, string DeviceId, EAlertType EAlertType, string Message, string DataRegistered, DateTime RegisteredAt );