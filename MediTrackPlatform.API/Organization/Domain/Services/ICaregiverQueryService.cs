using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;

namespace MediTrackPlatform.API.Organization.Domain.Services;

public interface ICaregiverQueryService
{
    Task<Caregiver?> Handle(GetCaregiverByIdQuery query);
    Task<IEnumerable<Caregiver>> Handle(GetAllCaregiversQuery query);
    Task<IEnumerable<Caregiver>> Handle(GetAllCaregiversByOrganizationIdQuery query);
    Task<Caregiver?> Handle(GetCaregiverByUserIdQuery query);
    Task<Caregiver?> Handle(GetCaregiverByUserIdAndOrganizationIdQuery query);
}