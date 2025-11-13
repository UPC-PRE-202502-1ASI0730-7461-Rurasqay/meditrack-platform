using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MediTrackPlatform.API.Organization.Infrastructure.Persistence.EFC.Repositories;

public class SeniorCitizenRepository(AppDbContext context)
    : BaseRepository<SeniorCitizen>(context), ISeniorCitizenRepository
{
    public async Task<IEnumerable<SeniorCitizen>> ListByOrganizationIdAsync(int organizationId)
        => await Context
            .Set<SeniorCitizen>()
            .Where(seniorCitizen => seniorCitizen.OrganizationId == organizationId)
            .ToListAsync();
}