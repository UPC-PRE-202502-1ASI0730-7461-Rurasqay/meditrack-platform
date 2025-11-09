
using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;
using MediTrackPlatform.API.Relatives.Domain.Model.Queries;

public interface IRelativeQueryService
{
    Task<Relative?> Handle(GetRelativesByIdQuery query);
}