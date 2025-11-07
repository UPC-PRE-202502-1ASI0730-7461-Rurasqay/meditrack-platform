namespace MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

public record BloodPressure(int Diastolic, int Systolic, DateTime MeasuredAt);