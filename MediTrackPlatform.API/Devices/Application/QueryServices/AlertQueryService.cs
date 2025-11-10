using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
using MediTrackPlatform.API.Devices.Domain.Model.Queries;
using MediTrackPlatform.API.Devices.Domain.Repositories;
using MediTrackPlatform.API.Devices.Domain.Services;

namespace MediTrackPlatform.API.Devices.Application.QueryServices;

public class AlertQueryService(IAlertRepository alertRepository) : IAlertQueryService
{
    public async Task<Alert?> Handle(GetAlertByIdQuery query)
    {
        return await alertRepository.FindByIdAsync(query.AlertId);
    }

    public async Task<IEnumerable<Alert>> Handle(GetAllAlertsQuery query)
    {
        return await alertRepository.ListBySeniorCitizenIdAsync(query.SeniorCitizenId);
    }
}