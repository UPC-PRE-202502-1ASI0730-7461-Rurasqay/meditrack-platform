using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Aggregates;

public partial class Device
{
    public int DeviceId { get; set; }
    public string Model { get; set; }
    public EDeviceStatus  Status { get; set; }
    public Holder Holder { get; set; }

    public Device(string model, int holderId) : this()
    {
        Model = model;
        Status = EDeviceStatus.Active;
        Holder = new Holder(holderId, "SeniorCitizen");
    }

    public Device(CreateDeviceCommand command) : this(command.Model, command.HolderId)
    {
        
    }
}