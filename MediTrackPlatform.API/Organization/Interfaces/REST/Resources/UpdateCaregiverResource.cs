namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record UpdateCaregiverResource(
    string FirstName,
    string LastName,
    int Age,
    string Email,
    string PhoneNumber,
    string ImageUrl
    );