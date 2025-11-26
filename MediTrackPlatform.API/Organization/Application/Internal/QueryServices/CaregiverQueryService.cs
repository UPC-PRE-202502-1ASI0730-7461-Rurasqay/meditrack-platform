using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;

namespace MediTrackPlatform.API.Organization.Application.Internal.QueryServices;

public class CaregiverQueryService(ICaregiverRepository caregiverRepository) : ICaregiverQueryService
{
    public async Task<Caregiver?> Handle(GetCaregiverByIdQuery query)
        => await caregiverRepository.FindByIdAsync(query.CaregiverId);

    public async Task<IEnumerable<Caregiver>> Handle(GetAllCaregiversQuery query)
        => await caregiverRepository.ListAsync();

    public async Task<IEnumerable<Caregiver>> Handle(GetAllCaregiversByOrganizationIdQuery query)
        => await caregiverRepository.ListByOrganizationIdAsync(query.OrganizationId);
    
    public async Task<Caregiver?> Handle(GetCaregiverByUserIdQuery query)
        => await caregiverRepository.FindByUserIdAsync(query.UserId);
    
    public async Task<Caregiver?> Handle(GetCaregiverByUserIdAndOrganizationIdQuery query)
        => await caregiverRepository.FindByUserIdAndOrganizationIdAsync(query.UserId, query.OrganizationId);
}