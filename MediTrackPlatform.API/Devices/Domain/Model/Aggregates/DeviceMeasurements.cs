using MediTrackPlatform.API.Devices.Domain.Model.Entities;
using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Aggregates;

public partial class Device
{
    public ICollection<Measurement> Measurements { get; set; }
    
    public Device()
    {
        Model = string.Empty;
        Measurements = [];
    }
    
    public void AddHeartRate(int bpm)
    {
        Measurements.Add(new HeartRateMeasurement(bpm));
    }
    
    public void AddTemperature(int celcius)
    {
        Measurements.Add(new TemperatureMeasurement(celcius));
    }
    
    public void AddOxygen(int spo2)
    {
        Measurements.Add(new OxygenMeasurement(spo2));
    }
    
    public void AddBloodPressure(int diastolic, int systolic)
    {
        Measurements.Add(new BloodPressureMeasurement(diastolic, systolic));
    }
    
    public void RemoveAsset(int measurementId)
    {
        var measurement = Measurements.FirstOrDefault(m => m.MeasurementId == measurementId);
        if (measurement is null) return;
        Measurements.Remove(measurement);
    }
}