namespace MediTrackPlatform.API.Relatives.Domain.Model.Commands;

public record CreateRelativeCommand(
    int? UserId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string PlanType,
    int SeniorCitizenId
);
