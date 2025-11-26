namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record UpdateCaregiverCommand(
    int CaregiverId,
    string FirstName,
    string LastName,
    int Age,
    string Email,
    string PhoneNumber,
    string ImageUrl
    );