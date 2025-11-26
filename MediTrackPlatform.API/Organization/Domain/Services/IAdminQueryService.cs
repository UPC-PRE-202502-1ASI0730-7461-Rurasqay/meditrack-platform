using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;

namespace MediTrackPlatform.API.Organization.Domain.Services;

public interface IAdminQueryService
{
    Task<Admin?> Handle(GetAdminByIdQuery query);
    Task<IEnumerable<Admin>> Handle(GetAllAdminsQuery query);
    Task<IEnumerable<Admin>> Handle(GetAllAdminsByOrganizationIdQuery query);
    Task<Admin?> Handle(GetAdminByUserIdAndOrganizationIdQuery query);
}