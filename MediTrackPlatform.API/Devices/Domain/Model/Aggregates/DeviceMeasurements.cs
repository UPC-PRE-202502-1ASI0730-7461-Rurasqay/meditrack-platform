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
        var measurement = new HeartRateMeasurement(bpm);
        Measurements.Add(measurement);
    }
    
    public void AddTemperature(double celsius)
    {
        var measurement = new TemperatureMeasurement(celsius);
        Measurements.Add(measurement);
    }
    
    public void AddOxygen(int spo2)
    {
        var measurement = new OxygenMeasurement(spo2);
        Measurements.Add(measurement);
    }
    
    public void AddBloodPressure(int diastolic, int systolic)
    {
        var measurement = new BloodPressureMeasurement(diastolic, systolic);
        Measurements.Add(measurement);
    }
    
    public ICollection<Measurement> GetMeasurementsOfType<T>() where T : Measurement
    {
        return Measurements.Where(m => m is T).ToList();
    }
    
    public ICollection<Measurement> GetHeartRateMeasurements()
    {
        return GetMeasurementsOfType<HeartRateMeasurement>();
    }
    
    public ICollection<Measurement> GetTemperatureMeasurements()
    {
        return GetMeasurementsOfType<TemperatureMeasurement>();
    }
    
    public ICollection<Measurement> GetOxygenMeasurements()
    {
        return GetMeasurementsOfType<OxygenMeasurement>();
    }
    
    public ICollection<Measurement> GetBloodPressureMeasurements()
    {
        return GetMeasurementsOfType<BloodPressureMeasurement>();
    }
    
    public bool ExistsMoreThanWeeklyMeasurementsOfType<T>() where T : Measurement
    {
        return Measurements.Count(m => m is T) > 7;
    }
    
    public void RemoveLastMeasurementOfType<T>() where T : Measurement
    {
        var lastMeasurement = Measurements.LastOrDefault(m => m is T);
        if (lastMeasurement != null)
        {
            Measurements.Remove(lastMeasurement);
        }
    }
}