namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record CaregiverResource(
    int Id,
    int UserId,
    int OrganizationId,
    string FirstName,
    string LastName,
    int Age,
    string Email,
    string PhoneNumber,
    string ImageUrl,
    DateTime? CreatedAt,
    DateTime? UpdatedAt
    );