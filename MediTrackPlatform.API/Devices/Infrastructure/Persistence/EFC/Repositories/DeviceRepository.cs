using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediTrackPlatform.API.Devices.Infrastructure.Persistence.EFC.Repositories;

public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
{
    public DeviceRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Device>> ListByHolderIdAsync(int holderId)
    {
        string holderIdString = holderId.ToString();

        return await Context.Set<Device>()
            .Where(d => d.Holder.HolderId == holderIdString)
            .ToListAsync();
    }
}