using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class CreateCaregiverCommandFromResourceAssembler
{
    public static CreateCaregiverCommand ToCommandFromResource(CreateCaregiverResource resource)
    {
        return new CreateCaregiverCommand(
            resource.UserId,
            resource.OrganizationId,
            resource.FirstName,
            resource.LastName,
            resource.Age,
            resource.Email,
            resource.PhoneNumber,
            resource.ImageUrl
            );
    }
}