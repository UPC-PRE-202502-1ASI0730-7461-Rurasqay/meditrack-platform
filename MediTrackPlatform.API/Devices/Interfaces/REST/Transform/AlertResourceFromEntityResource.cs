using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public static class AlertResourceFromEntityResource
{
    public static AlertResource ToResourceFromEntity(Alert entity)
    {
        return new AlertResource(
            entity.AlertId,
            entity.DeviceId,
            entity.EAlertType.ToString(),
            entity.Message,
            entity.DataRegistered,
            entity.RegisteredAt
        );
    }
}