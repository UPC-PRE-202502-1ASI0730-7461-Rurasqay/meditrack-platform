using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class AdminResourceFromEntityAssembler
{
    public static AdminResource ToResourceFromEntity(Admin entity)
    {
        return new AdminResource(
            entity.Id,
            entity.UserId,
            entity.OrganizationId,
            entity.FirstName,
            entity.LastName,
            entity.CreatedDate?.DateTime,
            entity.UpdatedDate?.DateTime
            );
    }
}