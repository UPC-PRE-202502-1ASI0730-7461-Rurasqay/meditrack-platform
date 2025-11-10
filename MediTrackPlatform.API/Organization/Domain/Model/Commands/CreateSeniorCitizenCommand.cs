namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record CreateSeniorCitizenCommand(
    int OrganizationId,
    int DeviceId,
    int AssignedDoctorId,
    int AssignedCaregiverId,
    string FirstName,
    string LastName,
    string Dni,
    string BirthDate,
    int Age,
    string Gender,
    double Weight,
    double Height,
    string ImageUrl
    );