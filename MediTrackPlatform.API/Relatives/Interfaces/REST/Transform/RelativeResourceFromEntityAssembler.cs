using MediTrackPlatform.API.Relatives.Domain.Model.Aggregates;
using MediTrackPlatform.API.Relatives.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Relatives.Interfaces.REST.Transform;

public class RelativeResourceFromEntityAssembler
{
    public static RelativeResource ToResourceFromEntity(Relative entity)
    {
        var seniorCitizenResource = new SeniorCitizenResource(
            entity.SeniorCitizen.SeniorCitizenId,
            entity.SeniorCitizen.FirstName,
            entity.SeniorCitizen.LastName,
            entity.SeniorCitizen.Dni,
            entity.SeniorCitizen.Gender,
            entity.SeniorCitizen.Height,
            entity.SeniorCitizen.Weight,
            entity.SeniorCitizen.BirthDate,
            entity.SeniorCitizen.ProfileImage,
            entity.SeniorCitizen.DeviceId
        );
        
        string plan = entity.Plan.ToString();

        return new RelativeResource(
            entity.Id,
            plan,
            entity.FirstName,
            entity.LastName,
            entity.PhoneNumber,
            entity.ProfileImage,
            seniorCitizenResource
        );
    }
}