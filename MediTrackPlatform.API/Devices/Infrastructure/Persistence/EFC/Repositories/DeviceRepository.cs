using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MediTrackPlatform.API.Devices.Infrastructure.Persistence.EFC.Repositories;

public class DeviceRepository(AppDbContext context) : BaseRepository<Device>(context), IDeviceRepository
{
    public new async Task<Device?> FindByIdAsync(int id)
    {
        return await Context.Set<Device>()
            .Include(d => d.Measurements)
            .FirstOrDefaultAsync(d => d.DeviceId == id);
    }
}
