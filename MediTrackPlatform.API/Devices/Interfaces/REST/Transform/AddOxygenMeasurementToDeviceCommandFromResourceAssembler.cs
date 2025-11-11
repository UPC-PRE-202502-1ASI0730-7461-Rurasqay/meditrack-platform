using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public class AddOxygenMeasurementToDeviceCommandFromResourceAssembler
{
    public static AddOxygenMeasurementToDeviceCommand ToCommandFromResource(
        AddOxygenMeasurementToDeviceResource resource, int deviceId)
    {
        return new AddOxygenMeasurementToDeviceCommand(resource.Spo2, deviceId);
    }
}

