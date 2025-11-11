using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Resources;
public record AlertResource(
    int AlertId, 
    int DeviceId, 
    string EAlertType, 
    string Message, 
    double DataRegistered, 
    DateTime RegisteredAt );