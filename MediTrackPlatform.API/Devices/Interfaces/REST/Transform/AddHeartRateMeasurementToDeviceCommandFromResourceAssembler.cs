using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public class AddHeartRateMeasurementToDeviceCommandFromResourceAssembler
{
    public static AddHeartRateMeasurementToDeviceCommand ToCommandFromResource(
        AddHeartRateMeasurementToDeviceResource resource, int deviceId)
    {
        return new AddHeartRateMeasurementToDeviceCommand(resource.Bpm, deviceId);
    }
}

