namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record UpdateDoctorCommand(
    int DoctorId,
    string FirstName,
    string LastName,
    int Age,
    string Email,
    string PhoneNumber,
    string Specialty,
    string ImageUrl);