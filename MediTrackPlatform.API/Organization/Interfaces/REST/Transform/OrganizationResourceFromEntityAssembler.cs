using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class OrganizationResourceFromEntityAssembler
{
    public static OrganizationResource ToResourceFromEntity(Domain.Model.Aggregates.Organization entity)
    {
        return new OrganizationResource(
            entity.Id,
            entity.Name,
            entity.Type,
            entity.CreatedDate?.DateTime,
            entity.UpdatedDate?.DateTime
            );
    }
}