using MediTrackPlatform.API.Relatives.Domain.Model.Commands;
using MediTrackPlatform.API.Relatives.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Relatives.Interfaces.REST.Transform;

public class CreateRelativeCommandFromResourceAssembler
{
    public static CreateRelativeCommand ToCommandFromResource(CreateRelativeResource resource)
    {
        return new CreateRelativeCommand(
            resource.UserId,
            resource.FirstName,
            resource.LastName,
            resource.PhoneNumber,
            resource.PlanType,
            resource.SeniorCitizenId
        );
    }
}
