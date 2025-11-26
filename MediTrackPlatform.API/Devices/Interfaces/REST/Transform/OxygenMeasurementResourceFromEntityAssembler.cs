using MediTrackPlatform.API.Devices.Domain.Model.Entities;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public class OxygenMeasurementResourceFromEntityAssembler
{
    public static OxygenMeasurementResource ToResourceFromEntity(OxygenMeasurement entity)
    {
        return new OxygenMeasurementResource(
            entity.MeasurementId, 
            entity.Spo2, 
            entity.MeasuredAt);
    }
}

