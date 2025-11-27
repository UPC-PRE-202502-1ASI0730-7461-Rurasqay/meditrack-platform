using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;
using MediTrackPlatform.API.Relatives.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MediTrackPlatform.API.Relatives.Infrastructure.Persistence.Repositories;

public class RelativeRepository(AppDbContext context) : BaseRepository<Relative>(context), IRelativeRepository
{
    public async Task<Relative?> GetByIdAsync(int id)
    {
        return await Context.Set<Relative?>()
            .Include(r => r.SeniorCitizen) 
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}