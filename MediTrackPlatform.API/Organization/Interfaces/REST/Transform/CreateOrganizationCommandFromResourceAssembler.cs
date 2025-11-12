using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class CreateOrganizationCommandFromResourceAssembler
{
    public static CreateOrganizationCommand ToCommandFromResource(CreateOrganizationResource resource)
    {
        return new CreateOrganizationCommand(
            resource.Name,
            resource.Type
            );
    }
}