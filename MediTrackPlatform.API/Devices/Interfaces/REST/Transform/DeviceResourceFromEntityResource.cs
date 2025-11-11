using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public static class DeviceResourceFromEntityResource
{
    public static DeviceResource ToResourceFromEntity(Device entity)
    {
        return new DeviceResource(
            entity.DeviceId,
            entity.Model,
            entity.Status,
            entity.Holder
        );
    }
}