using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public class AddTemperatureMeasurementToDeviceCommandFromResourceAssembler
{
    public static AddTemperatureMeasurementToDeviceCommand ToCommandFromResource(
        AddTemperatureMeasurementToDeviceResource resource, int deviceId)
    {
        return new AddTemperatureMeasurementToDeviceCommand(resource.Celsius, deviceId);
    }
}

