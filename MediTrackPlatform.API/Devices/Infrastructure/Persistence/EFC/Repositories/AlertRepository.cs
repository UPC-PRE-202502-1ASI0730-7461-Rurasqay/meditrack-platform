using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediTrackPlatform.API.Devices.Infrastructure.Persistence.EFC.Repositories;

public class AlertRepository : BaseRepository<Alert>, IAlertRepository
{
    public AlertRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Alert>> ListBySeniorCitizenIdAsync(int seniorCitizenId)
    {
        string holderIdString = seniorCitizenId.ToString();

        var deviceIdsAsInt = await Context.Set<Device>()
            .Where(d => d.Holder.HolderId == holderIdString)
            .Select(d => d.DeviceId)
            .ToListAsync();

        var deviceIdsAsString = deviceIdsAsInt.Select(id => id.ToString()).ToList();

        return await Context.Set<Alert>()
            .Where(a => deviceIdsAsString.Contains(a.DeviceId))
            .ToListAsync();
    }
}