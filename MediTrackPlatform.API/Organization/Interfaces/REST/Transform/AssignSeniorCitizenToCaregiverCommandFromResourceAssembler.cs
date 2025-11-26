using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class AssignSeniorCitizenToCaregiverCommandFromResourceAssembler
{
    public static AssignSeniorCitizenToCaregiverCommand ToCommandFromResource(AssignSeniorCitizenToCaregiverResource resource)
    {
        return new AssignSeniorCitizenToCaregiverCommand(
            resource.SeniorCitizenId,
            resource.CaregiverId
            );
    }
}