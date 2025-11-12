using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Services;

public interface IDoctorCommandService
{
    Task<int?> Handle(CreateDoctorCommand command);
    Task<Doctor?> Handle(UpdateDoctorCommand command);
    Task Handle(DeleteDoctorCommand command);
    Task<Doctor?> Handle(AssignSeniorCitizenToDoctorCommand command);
    Task<Doctor?> Handle(UnassignSeniorCitizenFromDoctorCommand command);
}