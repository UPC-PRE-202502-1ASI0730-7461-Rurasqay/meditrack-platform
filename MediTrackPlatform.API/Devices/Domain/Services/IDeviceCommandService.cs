using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Commands;

namespace MediTrackPlatform.API.Devices.Domain.Services;

public interface IDeviceCommandService
{
    Task<Device?> Handle(CreateDeviceCommand command);
    Task<Device?> Handle(AddBloodPressureMeasurementToDeviceCommand command);
    Task<Device?> Handle(AddHeartRateMeasurementToDeviceCommand command);
    Task<Device?> Handle(AddTemperatureMeasurementToDeviceCommand command);
    Task<Device?> Handle(AddOxygenMeasurementToDeviceCommand command);
}