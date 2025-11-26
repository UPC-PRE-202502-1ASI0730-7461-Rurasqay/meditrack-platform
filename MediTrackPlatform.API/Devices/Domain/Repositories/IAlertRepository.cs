using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Devices.Domain.Repositories;

using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
public interface IAlertRepository : IBaseRepository<Alert>
{
}