namespace MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

public record TemperatureMeasurementResource(
    int MeasurementId, 
    double Celsius, 
    DateTime MeasuredAt);

