namespace MediTrackPlatform.API.Organization.Domain.Model.Commands;

public record UnassignSeniorCitizenFromCaregiverCommand(int SeniorCitizenId, int CaregiverId);