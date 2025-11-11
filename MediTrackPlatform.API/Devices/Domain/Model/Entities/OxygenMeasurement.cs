using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Entities;

public class OxygenMeasurement : Measurement
{
    public OxygenMeasurement() : base(EMeasurementType.Oxygen)
    {
        Threshold = new OxygenThreshold(90);
    }

    public OxygenMeasurement(int spo2) : base(EMeasurementType.Oxygen)
    {
        Spo2 = spo2;
        Threshold = new OxygenThreshold(90);
    }
    
    public int Spo2 { get; set; }
    public OxygenThreshold Threshold { get; set; }
    
    public bool SurpassesThreshold()
    {
        return Threshold.IsViolatedBy(Spo2);
    }
}