using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Entities;

public class BloodPressureMeasurement : Measurement
{
    public BloodPressureMeasurement() : base(EMeasurementType.BloodPressure)
    {
        Threshold = new BloodPressureThreshold(90, 180);
    }

    public BloodPressureMeasurement(int diastolic, int systolic) : base(EMeasurementType.BloodPressure)
    {
        Threshold = new BloodPressureThreshold(90, 180);
        Diastolic = diastolic;
        Systolic = systolic;
    }
    
    public int Diastolic { get; set; }
    public int Systolic { get; set; }
    
    public BloodPressureThreshold Threshold { get; set; }
    
    public bool SurpassesThreshold()
    {
        return Threshold.IsViolatedBy(Diastolic) || Threshold.IsViolatedBy(Systolic);
    }
}