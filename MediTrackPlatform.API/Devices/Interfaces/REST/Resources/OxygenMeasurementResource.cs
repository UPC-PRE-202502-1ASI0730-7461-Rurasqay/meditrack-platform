namespace MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

public record OxygenMeasurementResource(
    int MeasurementId, 
    int Spo2, 
    DateTime MeasuredAt);

