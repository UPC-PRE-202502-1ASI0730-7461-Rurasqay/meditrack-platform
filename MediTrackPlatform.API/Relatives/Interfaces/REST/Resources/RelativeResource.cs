namespace MediTrackPlatform.API.Relatives.Interfaces.REST.Resources;

public record RelativeResource(
    int Id,
    string Plan,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string ProfileImage,
    SeniorCitizenResource SeniorCitizen
);