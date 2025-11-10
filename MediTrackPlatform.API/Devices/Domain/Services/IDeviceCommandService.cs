using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Commands;

namespace MediTrackPlatform.API.Devices.Domain.Services;

public interface IDeviceCommandService
{
    Task<Device?> Handle(CreateDeviceCommand command);
}