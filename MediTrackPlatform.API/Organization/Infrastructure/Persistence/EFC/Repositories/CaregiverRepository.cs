using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MediTrackPlatform.API.Organization.Infrastructure.Persistence.EFC.Repositories;

public class CaregiverRepository(AppDbContext context)
    : BaseRepository<Caregiver>(context), ICaregiverRepository
{
    public async Task<IEnumerable<Caregiver>> ListByOrganizationIdAsync(int organizationId)
        => await Context
            .Set<Caregiver>()
            .Where(caregiver => caregiver.OrganizationId == organizationId)
            .ToListAsync();
    
    public async Task<Caregiver?> FindByUserIdAsync(int userId)
        => await Context
            .Set<Caregiver>()
            .FirstOrDefaultAsync(caregiver => caregiver.UserId == userId);

    public async Task<Caregiver?> FindByUserIdAndOrganizationIdAsync(int userId, int organizationId)
        => await Context
            .Set<Caregiver>()
            .FirstOrDefaultAsync(caregiver =>  caregiver.UserId == userId && caregiver.OrganizationId == organizationId);
}