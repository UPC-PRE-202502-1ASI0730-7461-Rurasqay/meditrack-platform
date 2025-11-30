using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Domain.Model.Queries;
using MediTrackPlatform.API.Devices.Domain.Services;
using MediTrackPlatform.API.Devices.Interfaces.ACL;

namespace MediTrackPlatform.API.Devices.Application.ACL;

/// <summary>
/// Implementation of DevicesContextFacade
/// Provides ACL for external contexts to interact with the Devices bounded context
/// </summary>
/// <param name="deviceCommandService">The device command service</param>
/// <param name="deviceQueryService">The device query service</param>
public class DevicesContextFacade(
    IDeviceCommandService deviceCommandService,
    IDeviceQueryService deviceQueryService
) : IDevicesContextFacade
{
    public async Task<int> CreateDevice(string model, int holderId)
    {
        var createDeviceCommand = new CreateDeviceCommand(model, holderId);
        var device = await deviceCommandService.Handle(createDeviceCommand);
        return device?.DeviceId ?? 0;
    }

    public async Task<int> FetchDeviceIdByHolderId(int holderId)
    {
        // This would require adding a query method to find device by holderId
        // For now, returning 0 as this is primarily for creation
        return await Task.FromResult(0);
    }

    public async Task<bool> DeviceExists(int deviceId)
    {
        if (deviceId <= 0)
        {
            return false;
        }
        var query = new GetDeviceByIdQuery(deviceId);
        var device = await deviceQueryService.Handle(query);
        return device is not null;
    }
}

