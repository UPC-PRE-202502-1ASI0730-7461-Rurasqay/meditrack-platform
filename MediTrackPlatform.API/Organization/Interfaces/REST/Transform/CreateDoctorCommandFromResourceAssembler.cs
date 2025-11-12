using MediTrackPlatform.API.Organization.Domain.Model.Commands;

namespace MediTrackPlatform.API.Organization.Interfaces.REST.Transform;

public static class CreateDoctorCommandFromResourceAssembler
{
    public static CreateDoctorCommand ToCommandFromResource(CreateDoctorCommand command)
    {
        return new CreateDoctorCommand(
            command.UserId,
            command.OrganizationId,
            command.FirstName,
            command.LastName,
            command.Age,
            command.Email,
            command.PhoneNumber,
            command.Specialty,
            command.ImageUrl
            );
    }
}