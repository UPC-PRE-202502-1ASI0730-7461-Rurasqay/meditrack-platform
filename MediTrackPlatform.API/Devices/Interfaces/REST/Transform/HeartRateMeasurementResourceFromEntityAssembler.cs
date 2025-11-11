using MediTrackPlatform.API.Devices.Domain.Model.Entities;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public class HeartRateMeasurementResourceFromEntityAssembler
{
    public static HeartRateMeasurementResource ToResourceFromEntity(HeartRateMeasurement entity)
    {
        return new HeartRateMeasurementResource(
            entity.MeasurementId, 
            entity.Bpm, 
            entity.MeasuredAt);
    }
}

