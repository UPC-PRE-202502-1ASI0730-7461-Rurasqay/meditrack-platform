using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;
using MediTrackPlatform.API.Organization.Domain.Repositories;
using MediTrackPlatform.API.Organization.Domain.Services;

namespace MediTrackPlatform.API.Organization.Application.Internal.QueryServices;

public class DoctorQueryService(IDoctorRepository doctorRepository) : IDoctorQueryService
{
    public async Task<Doctor?> Handle(GetDoctorByIdQuery query)
        => await doctorRepository.FindByIdAsync(query.DoctorId);

    public async Task<IEnumerable<Doctor>> Handle(GetAllDoctorsQuery query)
        => await doctorRepository.ListAsync();

    public async Task<IEnumerable<Doctor>> Handle(GetAllDoctorsByOrganizationIdQuery query)
        => await doctorRepository.ListByOrganizationIdAsync(query.OrganizationId);

    public async Task<Doctor?> Handle(GetDoctorByUserIdQuery query)
        => await doctorRepository.FindByUserIdAsync(query.UserId);
    
    public async Task<Doctor?> Handle(GetDoctorByUserIdAndOrganizationIdQuery query)
        => await doctorRepository.FindByUserIdAndOrganizationIdAsync(query.UserId, query.OrganizationId);
}