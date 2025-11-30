namespace MediTrackPlatform.API.Relatives.Interfaces.REST.Resources;

public record CreateRelativeResource(
    int? UserId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string PlanType,
    int SeniorCitizenId
);
