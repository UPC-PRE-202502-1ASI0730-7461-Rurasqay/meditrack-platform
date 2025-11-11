namespace MediTrackPlatform.API.Devices.Domain.Model.Commands;

public record AddOxygenMeasurementToDeviceCommand(int Spo2, int DeviceId);