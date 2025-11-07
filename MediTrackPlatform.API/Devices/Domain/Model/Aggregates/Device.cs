using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using MediTrackPlatform.API.Devices.Domain.Model.ValueObjects;

namespace MediTrackPlatform.API.Devices.Domain.Model.Aggregates;

public partial class Device
{
    public int DeviceId { get; set; }
    public string Model { get; set; }
    public EDeviceStatus  Status { get; set; }
    public Holder Holder { get; set; }

    public Device(string model, string holderId)
    {
        this.Model = model;
        this.Status = EDeviceStatus.Active;
        this.Holder = new Holder(holderId, "SeniorCitizen");
    }
}