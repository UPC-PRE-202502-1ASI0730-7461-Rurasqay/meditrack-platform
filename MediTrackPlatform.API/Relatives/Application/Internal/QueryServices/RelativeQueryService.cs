using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;
using MediTrackPlatform.API.Relatives.Domain.Model.Queries;
using MediTrackPlatform.API.Relatives.Domain.Repositories;
using MediTrackPlatform.API.Relatives.Domain.Services;

namespace MediTrackPlatform.API.Relatives.Application.Internal.QueryServices;

public class RelativeQueryService( IRelativeRepository  relativeRepository) : IRelativeQueryService
{
     

    public Task<Relative?> Handle(GetRelativesByIdQuery query)
    {
        return relativeRepository.GetByIdAsync(query.id);
    }

    public Task<Relative?> Handle(GetRelativeByUserIdQuery query)
    {
        return relativeRepository.FindByUserIdAsync(query.UserId);
    }
}