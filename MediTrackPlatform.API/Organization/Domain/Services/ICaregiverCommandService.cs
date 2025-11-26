using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Domain.Services;

public interface ICaregiverCommandService
{
    Task<int?> Handle(CreateCaregiverCommand command);
    Task<Caregiver?> Handle(UpdateCaregiverCommand command);
    Task Handle(DeleteCaregiverCommand command);
    Task<Caregiver?> Handle(AssignSeniorCitizenToCaregiverCommand command);
    Task<Caregiver?> Handle(UnassignSeniorCitizenFromCaregiverCommand command);
}