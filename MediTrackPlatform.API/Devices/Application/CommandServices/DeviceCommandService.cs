using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Commands;
using MediTrackPlatform.API.Devices.Domain.Repositories;
using MediTrackPlatform.API.Devices.Domain.Services;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Devices.Application.CommandServices;

public class DeviceCommandService(IDeviceRepository deviceRepository, IUnitOfWork unitOfWork) 
    : IDeviceCommandService
{
    public async Task<Device?> Handle(CreateDeviceCommand command)
    {
        var device = new Device(command.Model, command.HolderId.ToString());
        
        try
        {
            await deviceRepository.AddAsync(device);
            
            await unitOfWork.CompleteAsync();
            
            return device;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the device: {e.Message}");
            return null;
        }
    }
}