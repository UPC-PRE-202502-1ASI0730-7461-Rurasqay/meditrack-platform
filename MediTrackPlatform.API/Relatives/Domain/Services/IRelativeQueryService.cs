
using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;
using MediTrackPlatform.API.Relatives.Domain.Model.Queries;

namespace MediTrackPlatform.API.Relatives.Domain.Services;

public interface IRelativeQueryService
{
    Task<Relative?> Handle(GetRelativesByIdQuery query);
    Task<Relative?> Handle(GetRelativeByUserIdQuery query);
}