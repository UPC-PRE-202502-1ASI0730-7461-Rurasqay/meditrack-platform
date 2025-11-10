using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Aggregates;

public partial class Alert
{
    public int AlertId { get; set; }
    public int DeviceId { get; set; }
    public EAlertType EAlertType { get; set; }
    public string Message { get; set; }
    public string DataRegistered { get; set; }
    public DateTime RegisteredAt { get; set; }

    public Alert(int deviceId, string dataRegistered)
    {
        this.DeviceId = deviceId;
        this.DataRegistered = dataRegistered;
        this.EAlertType = EAlertType.Danger;
        this.Message = string.Empty;
        this.RegisteredAt = DateTime.UtcNow;
    }

    public Alert(CreateAlertCommand command) : this(command.DeviceId, command.DataRegistered)
    {
        
    }
}