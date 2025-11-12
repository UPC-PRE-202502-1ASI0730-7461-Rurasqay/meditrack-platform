using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Services;

public interface ISeniorCitizenCommandService
{
    Task<int?> Handle(CreateSeniorCitizenCommand command);
    Task<SeniorCitizen?> Handle(UpdateSeniorCitizenCommand command);
    Task Handle(DeleteSeniorCitizenCommand command);
    Task<SeniorCitizen?> Handle(AssignSeniorCitizenToDoctorCommand command);
    Task<SeniorCitizen?> Handle(UnassignSeniorCitizenFromDoctorCommand command);
    Task<SeniorCitizen?> Handle(AssignSeniorCitizenToCaregiverCommand command);
    Task<SeniorCitizen?> Handle(UnassignSeniorCitizenFromCaregiverCommand command);
}