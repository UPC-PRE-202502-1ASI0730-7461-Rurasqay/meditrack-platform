namespace MediTrackPlatform.API.Relatives.Interfaces.REST.Resources;

public record SeniorCitizenResource(
    int Id,
    string FirstName,
    string LastName,
    string Dni,
    string Gender,
    double Height,
    string BirthDate,
    double Weight,
    string ProfileImage,
    int DeviceId
);