namespace MediTrackPlatform.API.Relatives.Interfaces.REST.Resources;

public record RelativeResource(
    int Id,
    string PlanType,
    string FirstName,
    string LastName,
    string PhoneNumber,
    SeniorCitizenResource SeniorCitizen
);