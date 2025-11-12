using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class AssignSeniorCitizenToDoctorCommandFromResourceAssembler
{
    public static AssignSeniorCitizenToDoctorCommand ToCommandFromResource(AssignSeniorCitizenToDoctorResource resource)
    {
        return new AssignSeniorCitizenToDoctorCommand(
            resource.SeniorCitizenId,
            resource.DoctorId
            );
    }
}