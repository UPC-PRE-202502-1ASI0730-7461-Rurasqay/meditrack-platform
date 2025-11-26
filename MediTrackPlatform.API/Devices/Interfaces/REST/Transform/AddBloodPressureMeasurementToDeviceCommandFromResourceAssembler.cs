using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public class AddBloodPressureMeasurementToDeviceCommandFromResourceAssembler
{
    public static AddBloodPressureMeasurementToDeviceCommand ToCommandFromResource(
        AddBloodPressureMeasurementToDeviceResource resource, int deviceId)
    {
        return new AddBloodPressureMeasurementToDeviceCommand(resource.Diastolic, resource.Systolic, deviceId);
    }
}