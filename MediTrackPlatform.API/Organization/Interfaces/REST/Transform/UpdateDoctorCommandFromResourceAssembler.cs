using MediTrackPlatform.API.Organization.Domain.Model.Commands;
using MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class UpdateDoctorCommandFromResourceAssembler
{
    public static UpdateDoctorCommand ToCommandFromResource(UpdateDoctorResource resource, int doctorId)
    {
        return new UpdateDoctorCommand(
            doctorId,
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