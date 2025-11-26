using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;

namespace MediTrackPlatform.API.Relatives.Domain.Repositories;

public interface IRelativeRepository
{
    Task<Relative?> GetByIdAsync(int id);
}