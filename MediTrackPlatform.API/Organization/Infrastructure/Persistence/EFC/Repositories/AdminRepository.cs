using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MediTrackPlatform.API.Organization.Infrastructure.Persistence.EFC.Repositories;

public class AdminRepository(AppDbContext context)
    : BaseRepository<Admin>(context), IAdminRepository
{
    public async Task<IEnumerable<Admin>> ListByOrganizationIdAsync(int organizationId)
        => await Context
            .Set<Admin>()
            .Where(admin => admin.OrganizationId == organizationId)
            .ToListAsync();

    public async Task<Admin?> FindByUserIdAndOrganizationIdAsync(int userId, int organizationId)
        => await Context
            .Set<Admin>()
            .FirstOrDefaultAsync(admin => admin.UserId == userId && admin.OrganizationId == organizationId);
}