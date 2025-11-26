using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Queries;

namespace MediTrackPlatform.API.Organization.Domain.Services;

public interface IDoctorQueryService
{
    Task<Doctor?> Handle(GetDoctorByIdQuery query);
    Task<IEnumerable<Doctor>> Handle(GetAllDoctorsQuery query);
    Task<IEnumerable<Doctor>> Handle(GetAllDoctorsByOrganizationIdQuery query);
    Task<Doctor?> Handle(GetDoctorByUserIdQuery query);
    Task<Doctor?> Handle(GetDoctorByUserIdAndOrganizationIdQuery query);
}