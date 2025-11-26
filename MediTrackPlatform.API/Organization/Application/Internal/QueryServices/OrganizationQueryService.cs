using MediTrackPlatform.API.Organization.Domain.Model.Queries;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;

namespace MediTrackPlatform.API.Organization.Application.Internal.QueryServices;

public class OrganizationQueryService(IOrganizationRepository organizationRepository) : IOrganizationQueryService
{
    public async Task<Domain.Model.Aggregates.Organization?> Handle(GetOrganizationByIdQuery query)
        => await organizationRepository.FindByIdAsync(query.OrganizationId);

    public async Task<IEnumerable<Domain.Model.Aggregates.Organization>> Handle(GetAllOrganizationsQuery query)
        => await organizationRepository.ListAsync();
}