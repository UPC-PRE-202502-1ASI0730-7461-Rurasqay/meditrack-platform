namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record UpdateSeniorCitizenCommand(
    int SeniorCitizenId,
    int DeviceId,
    string FirstName,
    string LastName,
    string Dni,
    string BirthDate,
    int Age,
    string Gender,
    double Weight,
    double Height,
    string ImageUrl
    );