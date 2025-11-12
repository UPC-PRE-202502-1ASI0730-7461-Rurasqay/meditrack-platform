namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record CreateDoctorResource(
    int UserId,
    int OrganizationId,
    string FirstName,
    string LastName,
    int Age,
    string Email,
    string PhoneNumber,
    string Specialty,
    string ImageUrl
    );