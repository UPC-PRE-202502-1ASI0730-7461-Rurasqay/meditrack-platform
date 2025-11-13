using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;

namespace MediTrackPlatform.API.Organization.Application.Internal.QueryServices;

public class AdminQueryService(IAdminRepository adminRepository) : IAdminQueryService
{
    public async Task<Admin?> Handle(GetAdminByIdQuery query)
        => await adminRepository.FindByIdAsync(query.AdminId);

    public async Task<IEnumerable<Admin>> Handle(GetAllAdminsQuery query)
        => await adminRepository.ListAsync();

    public async Task<IEnumerable<Admin>> Handle(GetAllAdminsByOrganizationIdQuery query)
        => await adminRepository.ListByOrganizationIdAsync(query.OrganizationId);

    public async Task<Admin?> Handle(GetAdminByUserIdAndOrganizationIdQuery query)
        => await adminRepository.FindByUserIdAndOrganizationIdAsync(query.UserId, query.OrganizationId);
}