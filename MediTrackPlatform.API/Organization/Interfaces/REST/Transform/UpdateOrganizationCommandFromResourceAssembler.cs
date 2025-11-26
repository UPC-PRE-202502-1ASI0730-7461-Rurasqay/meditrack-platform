using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class UpdateOrganizationCommandFromResourceAssembler
{
    public static UpdateOrganizationCommand ToCommandFromResource(UpdateOrganizationResource resource, int organizationId)
    {
        return new UpdateOrganizationCommand(
            organizationId,
            resource.Name,
            resource.Type
            );
    }
}