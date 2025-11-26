using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Entities;

public class TemperatureMeasurement: Measurement
{
    public TemperatureMeasurement() : base(EMeasurementType.Temperature)
    {
        Threshold = new TemperatureThreshold(35, 38);
    }

    public TemperatureMeasurement(double celcius) : base(EMeasurementType.Temperature)
    {
        Celcius = celcius;
        Threshold = new TemperatureThreshold(35.0, 38.0);
    }
    
    public double Celcius { get; set; }
    public TemperatureThreshold Threshold { get; set; }
    
    public bool SurpassesThreshold()
    {
        return Threshold.IsViolatedBy(Celcius);
    }
}