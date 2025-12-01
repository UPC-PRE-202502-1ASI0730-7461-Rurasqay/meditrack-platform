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
        return measurementType.ToUpper() switch
        {
            "TEMPERATURE" => DataRegistered > 38 
                ? $"Temperatura excedida: {DataRegistered:F1}°C (Normal: 35-38°C)" 
                : $"Temperatura debajo de valores normales: {DataRegistered:F1}°C (Normal: 35-38°C)",
            
            "HEART_RATE" or "HEARTRATE" => DataRegistered > 120 
                ? $"Frecuencia cardíaca elevada: {DataRegistered:F0} lpm (Normal: 50-120 lpm)" 
                : $"Frecuencia cardíaca baja: {DataRegistered:F0} lpm (Normal: 50-120 lpm)",
            
            "OXYGEN" => $"Saturación de oxígeno baja: {DataRegistered:F0}% (Normal: >90%)",
            
            "BLOOD_PRESSURE" or "BLOODPRESSURE" => DataRegistered > 180 
                ? $"Presión arterial elevada: {DataRegistered:F0} mmHg (Normal: 90-180 mmHg)" 
                : $"Presión arterial baja: {DataRegistered:F0} mmHg (Normal: 90-180 mmHg)",
            
            _ => $"Medición anormal detectada: {DataRegistered}"
        };
    }
}