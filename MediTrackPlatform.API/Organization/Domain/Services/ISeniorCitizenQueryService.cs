using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;

namespace MediTrackPlatform.API.Organization.Domain.Services;

public interface ISeniorCitizenQueryService
{
    Task<SeniorCitizen?> Handle(GetSeniorCitizenByIdQuery query);
    Task<IEnumerable<SeniorCitizen>> Handle(GetAllSeniorCitizensQuery query);
    Task<IEnumerable<SeniorCitizen>> Handle(GetAllSeniorCitizensByOrganizationIdQuery query);
}