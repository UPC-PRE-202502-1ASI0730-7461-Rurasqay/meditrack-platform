using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Aggregates;

public partial class Alert
{
    public int AlertId { get; set; }
    public int DeviceId { get; set; }
    public EAlertType EAlertType { get; set; }
    public string Message { get; set; }
    public double DataRegistered { get; set; }
    public DateTime RegisteredAt { get; set; }

    // Constructor sin parámetros para EF Core
    public Alert()
    {
        Message = string.Empty;
    }

    public Alert(int deviceId, double dataRegistered, string measurementType)
    {
        this.DeviceId = deviceId;
        this.DataRegistered = dataRegistered;
        this.EAlertType = EAlertType.Danger;
        this.Message = SetMessage(measurementType);
        this.RegisteredAt = DateTime.UtcNow;
    }

    public Alert(CreateAlertCommand command) : this(command.DeviceId, command.DataRegistered, command.MeasurementType)
    {
        
    }
    
    
    public string SetMessage(string measurementType)
    {
        return measurementType switch
        {
            "Temperature" => $"High temperature recorded: {DataRegistered}°C",
            "HeartRate" => $"Abnormal heart rate recorded: {DataRegistered} bpm",
            "Oxygen" => $"Low oxygen saturation recorded: {DataRegistered}%",
            "BloodPressure" => $"Abnormal blood pressure recorded: {DataRegistered} mmHg",
            _ => "Unknown measurement type"
        };
    }
}