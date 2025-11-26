using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Queries;

namespace MediTrackPlatform.API.Devices.Domain.Services;

public interface IAlertQueryService
{
    Task<Alert?> Handle(GetAlertByIdQuery query);
    
    Task<IEnumerable<Alert>> Handle(GetAllAlertsQuery query);
}