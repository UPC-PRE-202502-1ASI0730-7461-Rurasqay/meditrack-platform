using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Organization.Domain.Repositories;

public interface IDoctorRepository : IBaseRepository<Doctor>
{
    Task<IEnumerable<Doctor>> ListByOrganizationIdAsync(int organizationId);
    Task<Doctor?> FindByUserIdAsync(int userId);
    Task<Doctor?> FindByUserIdAndOrganizationIdAsync(int userId, int organizationId);
}