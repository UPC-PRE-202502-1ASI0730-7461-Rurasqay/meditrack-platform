namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record UpdateDoctorResource(
    string FirstName,
    string LastName,
    int Age,
    string Email,
    string Specialty,
    string PhoneNumber,
    string ImageUrl
    );