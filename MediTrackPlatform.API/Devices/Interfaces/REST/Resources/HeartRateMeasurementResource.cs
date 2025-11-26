namespace MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

public record HeartRateMeasurementResource(
    int MeasurementId, 
    int Bpm, 
    DateTime MeasuredAt);

