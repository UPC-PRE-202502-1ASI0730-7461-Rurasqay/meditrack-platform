using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public static class CreateDeviceCommandFromResource
{
    public static CreateDeviceCommand ToCommandFromResource(CreateDeviceResource resource)
    {
        return new CreateDeviceCommand( resource.Model, resource.HolderId );
    }
}