namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record SeniorCitizenResource(
    int Id,
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
    string PlanType,
    DateTime? CreatedAt,
    DateTime? UpdatedAt
    );