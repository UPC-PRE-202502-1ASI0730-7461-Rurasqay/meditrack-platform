using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;

namespace MediTrackPlatform.API.Organization.Application.Internal.QueryServices;

public class SeniorCitizenQueryService(ISeniorCitizenRepository seniorCitizenRepository) : ISeniorCitizenQueryService
{
    public async Task<SeniorCitizen?> Handle(GetSeniorCitizenByIdQuery query)
        => await seniorCitizenRepository.FindByIdAsync(query.SeniorCitizenId);
    
    public async Task<IEnumerable<SeniorCitizen>> Handle(GetAllSeniorCitizensQuery query)
        => await seniorCitizenRepository.ListAsync();

    public async Task<IEnumerable<SeniorCitizen>> Handle(GetAllSeniorCitizensByOrganizationIdQuery query)
        => await seniorCitizenRepository.ListByOrganizationIdAsync(query.OrganizationId);
}