using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;
using MediTrackPlatform.API.Relatives.Domain.Model.Queries;
using MediTrackPlatform.API.Relatives.Domain.Repositories;

namespace MediTrackPlatform.API.Relatives.Application.Internal.QueryServices;

public class RelativeQueryService( IRelativeRepository  relativeRepository) : IRelativeQueryService
{
     

    public Task<Relative?> Handle(GetRelativesByIdQuery query)
    {
        return relativeRepository.GetByIdAsync(query.id);
    }
}