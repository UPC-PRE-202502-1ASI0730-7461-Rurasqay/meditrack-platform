using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;
using MediTrackPlatform.API.Shared.Domain.Repositories;

namespace MediTrackPlatform.API.Relatives.Domain.Repositories;

public interface IRelativeRepository : IBaseRepository<Relative>
{
    Task<Relative?> GetByIdAsync(int id);
    Task<Relative?> FindByUserIdAsync(int userId);
}