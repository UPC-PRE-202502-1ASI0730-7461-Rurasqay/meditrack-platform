using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Queries;

namespace MediTrackPlatform.API.Devices.Domain.Services;

public interface IDeviceQueryService
{
    Task<Device?> Handle(GetDeviceByIdQuery query);
    
    Task<IEnumerable<Device>> Handle(GetAllDevicesQuery query);
}