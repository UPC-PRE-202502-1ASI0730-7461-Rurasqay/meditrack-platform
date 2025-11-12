using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class UpdateSeniorCitizenCommandFromResourceAssembler
{
    public static UpdateSeniorCitizenCommand ToCommandFromResource(UpdateSeniorCitizenResource resource, int seniorCitizenId)
    {
        return new UpdateSeniorCitizenCommand(
            seniorCitizenId,
            resource.DeviceId,
            resource.FirstName,
            resource.LastName,
            resource.Dni,
            resource.BirthDate,
            resource.Gender,
            resource.Weight,
            resource.Height,
            resource.ImageUrl
            );
    }
}