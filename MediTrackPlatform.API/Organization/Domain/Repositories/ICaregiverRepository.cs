using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Organization.Domain.Repositories;

public interface ICaregiverRepository : IBaseRepository<Caregiver>
{
    Task<IEnumerable<Caregiver>> ListByOrganizationIdAsync(int organizationId);
    Task<Caregiver?> FindByUserIdAsync(int userId);
    Task<Caregiver?> FindByUserIdAndOrganizationIdAsync(int userId, int organizationId);
}