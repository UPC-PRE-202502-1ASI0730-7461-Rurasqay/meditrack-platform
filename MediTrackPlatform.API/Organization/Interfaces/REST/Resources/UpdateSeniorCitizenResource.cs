namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record UpdateSeniorCitizenResource(
    int DeviceId,
    string FirstName,
    string LastName,
    string Dni,
    DateTime? BirthDate,
    string Gender,
    double Weight,
    double Height,
    string ImageUrl
    );