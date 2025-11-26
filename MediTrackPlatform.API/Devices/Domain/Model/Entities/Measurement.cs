using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Entities;

public abstract partial class Measurement
{
    public int MeasurementId { get; set; }
    public EMeasurementType Type { get; set; }
    public DateTime MeasuredAt { get; set; }

    // Constructor sin parámetros para EF Core
    protected Measurement()
    {
        MeasuredAt = DateTime.Now;
    }

    protected Measurement(EMeasurementType type) : this()
    {
        Type = type;
    }
}