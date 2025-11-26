using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Organization.Domain.Repositories;

public interface ISeniorCitizenRepository : IBaseRepository<SeniorCitizen>
{
    Task<IEnumerable<SeniorCitizen>> ListByOrganizationIdAsync(int organizationId);
}