using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class CreateDoctorCommandFromResourceAssembler
{
    public static CreateDoctorCommand ToCommandFromResource(CreateDoctorResource resource)
    {
        return new CreateDoctorCommand(
            resource.UserId,
            resource.OrganizationId,
            resource.FirstName,
            resource.LastName,
            resource.Age,
            resource.Email,
            resource.PhoneNumber,
            resource.Specialty,
            resource.ImageUrl
            );
    }
}