using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class UpdateCaregiverCommandFromResourceAssembler
{
    public static UpdateCaregiverCommand ToCommandFromResource(UpdateCaregiverResource resource, int caregiverId)
    {
        return new UpdateCaregiverCommand(
            caregiverId,
            resource.FirstName,
            resource.LastName,
            resource.Age,
            resource.Email,
            resource.PhoneNumber,
            resource.ImageUrl
            );
    }
}