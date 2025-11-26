namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record AssignSeniorCitizenToCaregiverResource(
    int SeniorCitizenId,
    int CaregiverId
    );