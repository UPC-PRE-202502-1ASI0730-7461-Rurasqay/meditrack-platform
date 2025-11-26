using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class CreateAdminCommandFromResourceAssembler
{
    public static CreateAdminCommand ToCommandFromResource(CreateAdminResource resource)
    {
        return new CreateAdminCommand(
            resource.UserId,
            resource.OrganizationId,
            resource.FirstName,
            resource.LastName
            );
    }
}