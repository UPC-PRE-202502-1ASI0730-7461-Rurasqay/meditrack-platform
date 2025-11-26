using MediTrackPlatform.API.Devices.Domain.Model.Entities;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public class TemperatureMeasurementResourceFromEntityAssembler
{
    public static TemperatureMeasurementResource ToResourceFromEntity(TemperatureMeasurement entity)
    {
        return new TemperatureMeasurementResource(
            entity.MeasurementId, 
            entity.Celcius, 
            entity.MeasuredAt);
    }
}

