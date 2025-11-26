using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class UpdateAdminCommandFromResourceAssembler
{
    public static UpdateAdminCommand ToCommandFromResource(UpdateAdminResource resource, int adminId)
    {
        return new UpdateAdminCommand(
            adminId,
            resource.FirstName,
            resource.LastName
            );
    }
}