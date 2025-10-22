using MediTrackPlatform.API.Shared.Domain.Repositories;
using MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace MediTrackPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}