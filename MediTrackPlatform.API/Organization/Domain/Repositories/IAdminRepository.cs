using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Organization.Domain.Repositories;

public interface IAdminRepository : IBaseRepository<Admin>
{
    Task<IEnumerable<Admin>> ListByOrganizationIdAsync(int organizationId);
    Task<Admin?> FindByUserIdAndOrganizationIdAsync(int userId, int organizationId);
}