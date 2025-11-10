using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;

namespace MediTrackPlatform.API.Devices.Domain.Repositories;

public interface IDeviceRepository
{
    Task AddAsync(Device device);

    Task<Device?> FindByIdAsync(int deviceId);
    
    Task<IEnumerable<Device>> ListByHolderIdAsync(int holderId);
    
    void Update(Device device);
    
}