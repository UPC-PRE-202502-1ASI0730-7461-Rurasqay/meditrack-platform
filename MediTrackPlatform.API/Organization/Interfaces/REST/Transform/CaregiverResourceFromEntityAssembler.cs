using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class CaregiverResourceFromEntityAssembler
{
    public static CaregiverResource ToResourceFromEntity(Caregiver entity)
    {
        return new CaregiverResource(
            entity.Id,
            entity.UserId,
            entity.OrganizationId,
            entity.FirstName,
            entity.LastName,
            entity.Age,
            entity.Email,
            entity.PhoneNumber,
            entity.ImageUrl,
            entity.CreatedDate?.DateTime,
            entity.UpdatedDate?.DateTime
            );
    }
}