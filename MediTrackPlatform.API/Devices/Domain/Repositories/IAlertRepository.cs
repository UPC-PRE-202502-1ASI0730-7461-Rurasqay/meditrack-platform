namespace MediTrackPlatform.API.Devices.Domain.Repositories;

using MediTrackPlatform.API.Devices.Domain.Model.Aggregates;
public interface IAlertRepository
{
    Task AddAsync(Alert alert);
    
    Task<Alert?> FindByIdAsync(int alertId);
    
    Task<IEnumerable<Alert>> ListBySeniorCitizenIdAsync(int seniorCitizenId);
}