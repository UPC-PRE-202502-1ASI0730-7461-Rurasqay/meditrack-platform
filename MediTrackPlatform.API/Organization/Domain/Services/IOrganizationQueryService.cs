using MediTrackPlatform.API.Organization.Domain.Model.Queries;

namespace MediTrackPlatform.API.Organization.Domain.Services;

public interface IOrganizationQueryService
{
    Task<Model.Aggregates.Organization> Handle(GetOrganizationByIdQuery query);
    Task<Model.Aggregates.Organization> Handle(GetAllOrganizationsQuery query);
}