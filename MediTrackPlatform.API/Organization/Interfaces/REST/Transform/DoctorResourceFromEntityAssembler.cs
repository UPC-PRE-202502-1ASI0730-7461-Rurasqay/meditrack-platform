using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class DoctorResourceFromEntityAssembler
{
    public static DoctorResource ToResourceFromEntity(Doctor entity)
    {
        return new DoctorResource(
            entity.Id,
            entity.UserId,
            entity.OrganizationId,
            entity.FirstName,
            entity.LastName,
            entity.Age,
            entity.Email,
            entity.PhoneNumber,
            entity.Specialty,
            entity.ImageUrl,
            entity.CreatedDate?.DateTime,
            entity.UpdatedDate?.DateTime
            );
    }
}