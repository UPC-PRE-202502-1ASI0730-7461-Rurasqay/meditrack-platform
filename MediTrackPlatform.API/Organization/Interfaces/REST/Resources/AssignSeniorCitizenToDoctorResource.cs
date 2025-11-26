namespace MediTrackPlatform.API.Organization.Interfaces.REST.Resources;

public record AssignSeniorCitizenToDoctorResource(
    int SeniorCitizenId,
    int DoctorId
    );