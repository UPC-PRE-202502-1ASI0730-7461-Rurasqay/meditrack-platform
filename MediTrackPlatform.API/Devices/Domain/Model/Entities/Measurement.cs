using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Entities;

public class Measurement
{
    public int SignalVitalsId { get; set; }
    public ICollection<HeartRate> HeartRateMeasurements { get; set; }
    public ICollection<BloodPressure> BloodPressureMeasurements { get; set; }
    public ICollection<Oxygen> OxygenSaturationMeasurements { get; set; }
    public ICollection<Temperature> TemperatureMeasurements { get; set; }
    
    public Measurement()
    {
        HeartRateMeasurements = [];
        BloodPressureMeasurements = [];
        OxygenSaturationMeasurements = [];
        TemperatureMeasurements = [];
    }
}