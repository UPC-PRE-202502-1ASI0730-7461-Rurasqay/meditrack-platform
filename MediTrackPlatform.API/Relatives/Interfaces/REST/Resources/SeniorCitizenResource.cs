namespace MediTrackPlatform.API.Relatives.Interfaces.REST.Resources;

public record SeniorCitizenResource(
    int SeniorCitizenId,
    string FirstName,
    string LastName,
    string Dni,
    string Gender,
    float Height,
    float Weight,
    DateTime BirthDate,
    string ProfileImage,
    string DeviceId
);