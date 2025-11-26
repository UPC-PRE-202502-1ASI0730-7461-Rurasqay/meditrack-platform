namespace MediTrackPlatform.API.Devices.Domain.Model.Commands;

public record AddHeartRateMeasurementToDeviceCommand(int Bpm, int DeviceId);