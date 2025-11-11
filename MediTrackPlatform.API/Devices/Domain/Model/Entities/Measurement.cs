using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Entities;

public partial class Measurement(EMeasurementType type)
{
    public int MeasurementId;
    public EMeasurementType Type;
    public DateTime MeasuredAt = DateTime.Now;
}