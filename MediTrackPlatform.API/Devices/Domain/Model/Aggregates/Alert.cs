using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Aggregates;

public partial class Alert
{
    public string AlertId { get; set; }
    public string DeviceId { get; set; }
    public EAlertType EAlertType { get; set; }
    public string Message { get; set; }
    public string DataRegistered { get; set; }
    public DateTime RegisteredAt { get; set; }

    public Alert()
    {
        
    }
}