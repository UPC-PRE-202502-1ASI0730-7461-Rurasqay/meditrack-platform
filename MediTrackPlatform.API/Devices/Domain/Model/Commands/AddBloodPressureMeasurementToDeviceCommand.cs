namespace MediTrackPlatform.API.Devices.Domain.Model.Commands;

public record AddBloodPressureMeasurementToDeviceCommand(int Diastolic, int Systolic, int DeviceId);