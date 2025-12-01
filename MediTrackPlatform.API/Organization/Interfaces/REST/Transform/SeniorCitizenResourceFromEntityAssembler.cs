using MediTrackPlatform.API.Organization.Domain.Model.Aggregates;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class SeniorCitizenResourceFromEntityAssembler
{
    public static SeniorCitizenResource ToResourceFromEntity(SeniorCitizen entity)
    {
        return new SeniorCitizenResource(
            entity.Id,
            entity.OrganizationId,
            entity.DeviceId,
            entity.AssignedDoctorId,
            entity.AssignedCaregiverId,
            entity.FirstName,
            entity.LastName,
            entity.Dni,
            entity.BirthDate,
            entity.Gender,
            entity.Weight,
            entity.Height,
            entity.ImageUrl,
            entity.PlanType,
            entity.CreatedDate?.DateTime,
            entity.UpdatedDate?.DateTime
            );
    } 
}