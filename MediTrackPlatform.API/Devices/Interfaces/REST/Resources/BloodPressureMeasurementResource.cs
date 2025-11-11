namespace MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

public record BloodPressureMeasurementResource(
    int MeasurementId, 
    int Diastolic, 
    int Systolic, 
    DateTime MeasuredAt);

