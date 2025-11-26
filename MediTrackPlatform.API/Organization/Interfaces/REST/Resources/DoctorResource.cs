namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record DoctorResource(
    int Id,
    int UserId,
    int OrganizationId,
    string FirstName,
    string LastName,
    int Age,
    string Email,
    string PhoneNumber,
    string Specialty,
    string ImageUrl,
    DateTime? CreatedAt,
    DateTime? UpdatedAt
    );