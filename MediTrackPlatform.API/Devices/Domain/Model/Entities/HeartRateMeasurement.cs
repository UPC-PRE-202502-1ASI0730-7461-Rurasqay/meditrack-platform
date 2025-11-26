using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Entities;

public class HeartRateMeasurement : Measurement
{
    public HeartRateMeasurement() : base(EMeasurementType.HeartRate)
    {
        Threshold = new HeartRateThreshold(50, 120);
    }

    public HeartRateMeasurement(int bpm) : base(EMeasurementType.HeartRate)
    {
        Bpm = bpm;
        Threshold = new HeartRateThreshold(50, 120);
    }
    
    public int Bpm { get; set; }
    
    public HeartRateThreshold Threshold { get; set; }
    
    public bool SurpassesThreshold()
    {
        return Threshold.IsViolatedBy(Bpm);
    }
}