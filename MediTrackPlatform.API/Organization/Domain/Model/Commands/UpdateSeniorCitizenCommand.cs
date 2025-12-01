namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record UpdateSeniorCitizenCommand(
    int SeniorCitizenId,
    int OrganizationId,
    int DeviceId,
    int AssignedDoctorId,
    int AssignedCaregiverId,
    string FirstName,
    string LastName,
    string Dni,
    DateTime? BirthDate,
    string Gender,
    double Weight,
    double Height,
    string ImageUrl,
    string PlanType
    );