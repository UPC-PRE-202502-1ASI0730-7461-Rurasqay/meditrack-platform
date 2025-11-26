namespace MediTrackPlatform.API.Devices.Domain.Model.Commands;

public record AddTemperatureMeasurementToDeviceCommand(double Celsius, int DeviceId);