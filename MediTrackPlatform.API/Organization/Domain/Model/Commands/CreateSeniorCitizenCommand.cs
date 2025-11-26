namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record CreateSeniorCitizenCommand(
    int OrganizationId,
    int DeviceId,
    string FirstName,
    string LastName,
    string Dni,
    DateTime? BirthDate,
    string Gender,
    double Weight,
    double Height,
    string ImageUrl
    );