using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class CreateSeniorCitizenCommandFromResourceAssembler
{
    public static CreateSeniorCitizenCommand ToCommandFromResource(CreateSeniorCitizenResource resource)
    {
        return new CreateSeniorCitizenCommand(
            resource.OrganizationId,
            resource.DeviceId,
            resource.FirstName,
            resource.LastName,
            resource.Dni,
            resource.BirthDate,
            resource.Gender,
            resource.Weight,
            resource.Height,
            resource.ImageUrl
            );
    }
}