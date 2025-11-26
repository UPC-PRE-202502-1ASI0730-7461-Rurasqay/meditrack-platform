using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MediTrackPlatform.API.Organization.Infrastructure.Persistence.EFC.Repositories;

public class DoctorRepository(AppDbContext context)
    : BaseRepository<Doctor>(context), IDoctorRepository
{
    public async Task<IEnumerable<Doctor>> ListByOrganizationIdAsync(int organizationId)
        => await Context
            .Set<Doctor>()
            .Where(doctor => doctor.OrganizationId == organizationId)
            .ToListAsync();
    
    public async Task<Doctor?> FindByUserIdAsync(int userId)
        => await Context
            .Set<Doctor>()
            .FirstOrDefaultAsync(doctor => doctor.UserId == userId);

    public async Task<Doctor?> FindByUserIdAndOrganizationIdAsync(int userId, int organizationId)
        => await Context
            .Set<Doctor>()
            .FirstOrDefaultAsync(doctor => doctor.UserId == userId && doctor.OrganizationId == organizationId); 
}