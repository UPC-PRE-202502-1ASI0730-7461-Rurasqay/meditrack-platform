using MediTrackPlatform.API.Devices.Domain.Model.Entities;
using MediTrackPlatform.API.Devices.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Devices.Interfaces.REST.Transform;

public class BloodPressureMeasurementResourceFromEntityAssembler
{
    public static BloodPressureMeasurementResource ToResourceFromEntity(BloodPressureMeasurement entity)
    {
        return new BloodPressureMeasurementResource(
            entity.MeasurementId, 
            entity.Diastolic, 
            entity.Systolic, 
            entity.MeasuredAt);
    }
}

